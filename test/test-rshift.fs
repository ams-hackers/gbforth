[asm]

: test
  $ab00 $8 rshift
  $cd00 $0 rshift ;

main: ' test # call,
begin, halt, repeat,

[endasm]
