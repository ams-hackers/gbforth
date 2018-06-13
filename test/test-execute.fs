[asm]


: test
  ['] drop execute
  ['] drop execute ;

main:

$11 push-lit,
$22 push-lit,
$33 push-lit,
' test # call,

begin, halt, repeat,

[endasm]
