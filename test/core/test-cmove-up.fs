( initialize )
: initialize
  0 $C100 c!
  1 $C101 c!
  2 $C102 c!
  3 $C103 c!
  4 $C104 c!
  5 $C105 c!
  6 $C106 c!
  0 $C107 c!
  0 $C108 c!
;

: main
  initialize
  $C101 $C103 #5 cmove> ;
