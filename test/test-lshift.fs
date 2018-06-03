[asm]

main:
$ab ps-push-lit,
$8 ps-push-lit,
' lshift # call,

$cd ps-push-lit,
$0 ps-push-lit,
' lshift # call,

label loop
halt,
loop jr,

[endasm]
