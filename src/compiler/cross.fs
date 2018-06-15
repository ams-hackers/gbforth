require ../rom.fs
require ../sym.fs
require ../utils/memory.fs

require ./ir.fs
require ./code.fs
require ./codegen.fs

require ./xname.fs

( Cross Dictionary )

wordlist constant xwordlist

-1 value xlatest
: create-xname ( addr flag -- )
  get-current >r
  xwordlist set-current
  make-xname
  dup IS xlatest
  create ,
  r> set-current ;

: ximmediate-as
  latest name>int F_IMMEDIATE create-xname ;

: find-xname ( addr u -- xname )
  2>r
  get-order
  xwordlist 1 set-order
  2r>

  find-name dup if name>int >body @ then

  >r
  set-order
  r> ;

\ for debugging
: xwords
  xwordlist >order words previous ;


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
-1 value current-ir

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
  xname' ?dup if
    dup cr hex. ." :"
    dup xprimitive? if
      cr ."   (code)"
    else
      >xcode .ir
    then
  else
    -1 abort" Unknown word"
  then ;

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
  make-ir dup to current-ir to current-node
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
  current-ir 0 create-xname

  ( flags ) WORD_NONAME = if
    xlatest xname>addr
  then

  -1 to current-ir
  -1 to current-node ; ximmediate-as ;


( Code definitions )

: code
  parse-user-name
  (code) ;

: end-code
  postpone (end-code)
  >r nextname
  r> F_PRIMITIVE create-xname
; immediate
