[asm]

: test
  $1234 $5678 =
  $5678 $1234 =
  $aa55 $aa88 =
  $aa88 $aa55 =
  $abcd $abcd = ;

main: ' test # call,
begin, halt, repeat,

[endasm]
