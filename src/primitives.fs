require ./asm.fs
require ./cross.fs

also gb-assembler

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

( a b -- b a )
code swap
ps-over-de,
ps-push-de,
ret,

( x -- )
code drop
ps-drop,
ret,

( a b -- c )
code +
ps-over-de,
DE HL add,
ret,

( c-addr -- x )
code c@
[HL] L ld,
$0 # H ld,
ret,

( x c-addr -- )
code c!
ps-over-de,
E [HL] ld,
ps-drop,
ret,

previous
