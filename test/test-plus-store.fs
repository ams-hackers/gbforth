[asm]

: test
  $ba65 $8501 !
  $1234 $8501 +! ;

main: ' test # call,
begin, halt, repeat,

[endasm]
