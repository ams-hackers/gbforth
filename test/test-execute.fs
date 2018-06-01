[asm]


: test
  ['] drop execute
  ['] drop execute ;

main:
ps-init,

$11 ps-push-lit,
$22 ps-push-lit,
$33 ps-push-lit,
' test # call,

label loop
halt,
loop jr,

[endasm]
