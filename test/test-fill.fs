[asm]


( initialize )
: initialize
  1 $C101 c!
  2 $C102 c!
  3 $C103 c!
  4 $C104 c!
  5 $C105 c!
  6 $C106 c!
  7 $C107 c!
  8 $C108 c!
;

main:

' initialize # call,

$C103 ps-push-lit,
#3 ps-push-lit,
#42 ps-push-lit,
' fill # call,

begin, halt, repeat,

[endasm]
