require ../rom.fs
require ../sym.fs

require ./ir.fs
require ./codegen.fs

( Cross words )

1 constant F_IMMEDIATE
2 constant F_CONSTANT

struct
  cell% field xname-flags
  cell% field xname-addr
  cell% field xname-ir
end-struct xname%

: make-xname ( addr flag -- xname )
  >r >r
  xname% %allot
  r> over xname-addr !
  r> over xname-flags ! ;

: >xcode xname-addr @ ;
: >xflags xname-flags @ ;
: ximmediate? >xflags F_IMMEDIATE and 0<> ;
: xconstant?  >xflags F_CONSTANT and 0<> ;


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

\ Create a cross-word, reading its name from the input stream using
\ `create-xname`.
: xcreate
  rom-offset dup
  parse-name 2dup nextname
  rot sym
  0 create-xname ;

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
  insert-node IR_NODE_LITERAL n mutate-node
  to current-node ;

: xcompile, { addr -- }
  current-node
  insert-node IR_NODE_CALL addr mutate-node
  to current-node ;

: xreturn,
  current-node
  insert-node IR_NODE_RET 0 mutate-node
  to current-node ;

: process-xname ( xname -- )
  dup ximmediate? if
    >xcode execute
  else
    dup xconstant? if
      >xcode xliteral,
    else
      >xcode xcompile,
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

: x;
  xreturn,
  x[
  current-ir xlatest xname-ir !
  current-ir gen-code
  -1 to current-ir
  -1 to current-node ; ximmediate-as ;

: xliteral xliteral, ; ximmediate-as literal

: x\ postpone \ ; ximmediate-as \
: x( postpone ( ; ximmediate-as (

: x]
  1 xstate !
  begin
    parse-next-name
    process-token
    xstate @ while
  repeat ;

: x'
  parse-next-name find-xname ?dup if >xcode else -1 abort" Unknown word " then ;

: x['] x' xliteral, ; ximmediate-as [']


create colon-name 128 chars allot

( Copy a string into colon-name to persist it! )
: copy-colon-name ( addr u -- addr' u )
  dup 128 >= abort" Name is too large!"
  dup >r colon-name swap move
  colon-name r> ;

: parse-colon-name
  parse-next-name copy-colon-name ;

( -- offset )
: create-word
  make-ir dup to current-ir to current-node
  rom-offset >r
  x]
  r@ 0 create-xname
  r> ;

( -- offset )
: x:noname
  noname create-word ;

( create the word AFTER parsing the definition so word is not visible to itself )
: x:
  parse-colon-name 2>r
  2r@ nextname create-word
  ( offset ) 2r> rot sym ;