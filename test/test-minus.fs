[asm]

main:

$abcd ps-push-lit,
$1234 ps-push-lit,
' - # call,

$89ab ps-push-lit,
$89ab ps-push-lit,
' - # call,

$AAAA ps-push-lit,
$AAAB ps-push-lit,
' - # call,

label loop
halt,
loop jr,

[endasm]
