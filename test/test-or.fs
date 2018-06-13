[asm]

: test
  $abcd $ff00 or
  $abcd $ff or ;

main: ' test # call,
begin, halt, repeat,

[endasm]
