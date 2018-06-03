[asm]

main:
$ab00 ps-push-lit,
$8 ps-push-lit,
' rshift # call,

$cd00 ps-push-lit,
$0 ps-push-lit,
' rshift # call,

label loop
halt,
loop jr,

[endasm]
