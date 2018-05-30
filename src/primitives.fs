require ./asm.fs
require ./cross.fs

[asm]

( TEMPORARY HACK: Don't break the game! )
$4400 ==>

: code
  parse-name
  2dup rom-offset sym
  nextname xcreate ;

( x -- x x )
code dup
ps-dup,
ret,

( x -- )
code drop
ps-drop,
ret,

( a b -- b a )
code swap
ps-swap,
ret,

( a b -- b )
code nip
C inc,
C inc,
ret,

( a b -- a b a )
code over
ps-over,
ret,

( a b -- b a b )
code tuck
ps-swap,
ps-over,
ret,

( a b c -- b c a )
code rot
ps-pop-de,
ps-swap,
ps-push-de,
ps-swap,
ret,

( a b c -- c a b )
code -rot
ps-swap,
ps-pop-de,
ps-swap,
ps-push-de,
ret,

( a b -- c )
code +
ps-over-de-nip,
DE HL add,
ret,

( c-addr -- x )
code c@
[HL] L ld,
$0 # H ld,
ret,

( x c-addr -- )
code c!
ps-over-de-nip,
E [HL] ld,
ps-drop,
ret,

code execute
\ The stack contains the return address for execute. We put HL on top,
\ so RET will take it and jump there.
hl push,
( but just before that, we have to drop the address from the data stack )
ps-drop,
ret,

[endasm]
