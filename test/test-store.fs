[asm]

: test
  $1234 $abcd
  $8501 !
  $8503 ! ;

main: ' test # call,
begin, halt, repeat,

[endasm]
