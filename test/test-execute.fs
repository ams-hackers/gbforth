[asm]


: test
  ['] drop execute
  ['] drop execute ;

main:

$11 ps-push-lit,
$22 ps-push-lit,
$33 ps-push-lit,
' test # call,

begin, halt, repeat,

[endasm]
