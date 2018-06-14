[asm]


( initialize )
: initialize
  1 $C101 c!
  2 $C102 c!
  3 $C103 c!
  4 $C104 c!
  5 $C105 c!
  6 $C106 c!

  0 $C201 c!
  0 $C202 c!
  0 $C203 c!
  0 $C204 c!
  0 $C205 c!
  0 $C206 c!
;

: test
  initialize
  $C101 $C201 #5 cmove ;

main: ' test # call,
ret,

[endasm]
