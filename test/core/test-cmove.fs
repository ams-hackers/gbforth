( initialize )
: initialize
  0 $C0FE c!
  0 $C0FF c!
  0 $C100 c!
  1 $C101 c!
  2 $C102 c!
  3 $C103 c!
  4 $C104 c!
  5 $C105 c!
  6 $C106 c!
;

: main
  initialize
  $C101 $C0FF #5 cmove ;
