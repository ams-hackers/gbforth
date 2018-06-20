require ../rom.fs
require ../sym.fs
require ../utils/memory.fs

require ./ir.fs
require ./code.fs
require ./codegen.fs

require ./xname.fs

( Constants )
: xconstant ( n -- )
  F_CONSTANT create-xname ;

( Cross Compiler )

\ Read the next word available in the inputs stream. Automatically
\ refill the stream if needed.
: parse-next-name
  parse-name dup if
  else
    refill 0= throw
    2drop recurse
  then ;

-1 value current-node

: xcompiling?
  current-node -1 <> ;

: xliteral, { n -- }
  current-node
  insert-node IR_NODE_LITERAL ::type n ::value
  to current-node ;

: xcompile, { xname -- }
  current-node
  insert-node IR_NODE_CALL ::type xname ::value
  to current-node ;

: xreturn,
  current-node
  insert-node IR_NODE_RET ::type
  to current-node ;

: process-xname ( xname -- )
  dup ximmediate? if
    >xcode execute
  else
    dup xconstant? if
      >xcode xliteral,
    else
      xcompile,
    then
  then ;

: process-token ( addr u -- )
  2dup find-xname ?dup if
    nip nip process-xname
  else
    s>number? if
      drop xliteral,
    else
      -1 abort" Unknown word"
    then
  then ;


( 0 if we the host is interpreting words,
 -1 if we are compiling into the target )
variable xstate

: x[ 0 xstate ! ; ximmediate-as [

: xliteral xliteral, ; ximmediate-as literal

: x\ postpone \ ; ximmediate-as \
: x( postpone ( ; ximmediate-as (
\ The next parenthesis is only here to make the editor happy!
)

: x]
  1 xstate !
  begin
    parse-next-name
    process-token
    xstate @ while
  repeat ;

: xname' 
  parse-next-name find-xname ;

: xsee
  cr
  xname' ?dup if
    dup ." ========== " .xname ." ( " dup hex. ." ) " ." ========== "
    dup xprimitive? if
      cr ." (code)" drop
    else
      >xcode .ir
    then
  else
    -1 abort" Unknown word"
  then 
  cr ;

: x'
  xname' ?dup if
    xname>addr
  else
    -1 abort" Unknown word "
  then ;

: x['] x' xliteral, ; ximmediate-as [']


create user-name 128 chars allot

( Copy a string into colon-name to persist it! )
: copy-user-name ( addr u -- addr' u )
  dup 128 >= abort" Name is too large!"
  dup >r user-name swap move
  user-name r> ;

: parse-user-name
  parse-next-name copy-user-name ;


1 constant WORD_NONAME
0 constant WORD_NAMED

: create-word
  make-ir to current-node
  current-node
  x] ;

: x:noname
  WORD_NONAME
  noname create-word ;

( create the word AFTER parsing the definition so word is not visible
( to itself, in x; )
: x:
  WORD_NAMED
  parse-user-name nextname create-word ;

: x; (  -- )
  xreturn,
  x[

  dup compute-ir-topological-order
  ( original-node ) 0 create-xname

  ( flags ) WORD_NONAME = if
    xlatest xname>addr
  then

  -1 to current-node ;


( Conditionals

                    consequent
                   /
                 _v______
  current-node  /        \    continuation
         _v____/          \__v____
             ^ \          /
             |  \________/ <---- IR_NODE_CONTINUE
             |    ^
             |    |
             |    | alternative
             |
             IR_NODE_FORK
)

: xif ( -- alternative )
  make-ir make-ir { consequent alternative }

  current-node
  insert-node IR_NODE_FORK ::type consequent ::value alternative ::value'
  drop

  consequent to current-node
  alternative

; ximmediate-as if


: xelse { alternative -- continuation }
  make-ir { continuation }

  current-node 
  insert-node IR_NODE_CONTINUE ::type continuation ::value
  drop
  
  alternative to current-node
  continuation
; ximmediate-as else


\ Note that if xelse is not present, the continuation IR is just the
\ alternative IR from xif.
: xthen { continuation -- }
  current-node
  insert-node IR_NODE_CONTINUE ::type continuation ::value
  drop

  continuation to current-node
; ximmediate-as then


( Loops )

: xbegin { -- pre }
  make-ir { pre }
  current-node
  insert-node IR_NODE_CONTINUE ::type pre ::value
  drop
  pre to current-node
  pre
; ximmediate-as begin

: xwhile { pre -- pre continuation }
  make-ir make-ir { continuation post }
  current-node
  insert-node IR_NODE_FORK ::type post ::value continuation ::value'
  drop
  post to current-node
  pre continuation
; ximmediate-as while

: xrepeat { pre continuation -- }
  current-node
  insert-node IR_NODE_CONTINUE ::type pre ::value
  drop
  continuation to current-node
; ximmediate-as repeat


: xagain { body -- }
  current-node
  insert-node IR_NODE_CONTINUE ::type body ::value
  drop
  \ Code following the next IR component is unreachable! but we have
  \ to collect it somewhere
  make-ir to current-node
; ximmediate-as again


( Code definitions )

[asm]

: code
  parse-user-name
  (code) ;

: -end-code
  postpone (end-code)
  >r nextname
  r> F_PRIMITIVE create-xname
; immediate

: end-code
  postpone ret,
  postpone -end-code
; immediate

[endasm]
