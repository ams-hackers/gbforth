require ./rom.fs
require ./kernel.fs

[asm]

( Cross words )

1 constant F_IMMEDIATE
2 constant F_CONSTANT

struct
  cell% field xname-flags
  cell% field xname-addr
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

: create-xname ( addr flag -- )
  get-current >r
  xwordlist set-current
  make-xname create ,
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

: process-number ( n -- )
  xliteral, ;

: process-word ( addr u -- )
  2dup find-xname ?dup if
    nip nip process-xname
  else
    s>number? if
      drop process-number
    else
      -1 abort" Unknown word"
    then
  then ;


( 0 if we the host is interpreting words,
 -1 if we are compiling into the target )
variable xstate

: x[ 0 xstate ! ; ximmediate-as [
: x; x[ xreturn, ; ximmediate-as ;

: xliteral xliteral, ; ximmediate-as literal

: x\ postpone \ ; ximmediate-as \
: x( postpone ( ; ximmediate-as (

: x]
  1 xstate !
  begin
    parse-next-name
    process-word
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

: x:
  rom-offset dup >r
  parse-colon-name 2dup 2>r
  rot sym
  x]
  2r> nextname
  r> 0 create-xname ;

[endasm]
