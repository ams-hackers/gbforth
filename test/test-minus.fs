[asm]

main:

$abcd ps-push-lit,
$1234 ps-push-lit,
' - # call,

label loop
halt,
loop jr,

[endasm]
