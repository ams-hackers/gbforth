[asm]

: test
  $abcd $ff00 and
  $abcd $ff and ;

main: ' test # call,
begin, halt, repeat,

[endasm]
