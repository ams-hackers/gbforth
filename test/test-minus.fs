[asm]

: test
  $abcd $1234 -
  $89ab $89ab -
  $aaaa $aaab - ;

main: ' test # call,
ret,

[endasm]
