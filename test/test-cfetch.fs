[asm]

: test
  $0104 c@   \ ce
  $0105 c@   \ ed
  $0106 c@ ; \ 66

main: ' test # call,
begin, halt, repeat,

[endasm]
