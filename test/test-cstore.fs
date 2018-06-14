[asm]

: test
  $66 $ed $ce
  $8501 c!
  $8502 c!
  $8503 c! ;

main: ' test # call,
ret,

[endasm]
