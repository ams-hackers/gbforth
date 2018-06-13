[asm]

: test
  $1234 $5678 max
  $abcd $6789 max
  $aa55 $aa88 max
  $aa44 $aa22 max ;

main: ' test # call,
begin, halt, repeat,

[endasm]
