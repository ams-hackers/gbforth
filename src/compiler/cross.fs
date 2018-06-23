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
  parse-next-name find-xname
  dup 0= if
    -1 abort" Unknown word"
  then ;

: xpostpone, ( xname -- )
  postpone literal
  postpone process-xname ;

: xsee
  cr
  xname'
  ." ========== " dup .xname ." ( " dup hex. ." ) " ." ========== "
  dup xprimitive? if
    cr ." (code)" drop
  else
    >xcode .ir
  then
  cr ;

: x'
  xname' xname>addr ;

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


( Control Flow )

make-ir constant unreachable

\ Create a unresolved mark.
: @there ( -- mark )
  make-ir ;

\ Compile a jump to a mark. Code compiled directly afte rthe mark
\ becomes unreachable.
: branch, { mark -- }
  current-node
  insert-node IR_NODE_CONTINUE ::type mark ::value
  drop
  unreachable to current-node ;

\ Compile a jump to a mark if the top of the stack is zero.
: 0branch, { mark -- }
  make-ir { continue }
  current-node
  insert-node IR_NODE_FORK ::type continue ::value mark ::value'
  drop
  continue to current-node ;

\ Resolve a mark to the current location.
: @resolve ( mark -- )
  dup branch, to current-node ;



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
