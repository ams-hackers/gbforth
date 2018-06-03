[asm]

main:

$5678 ps-push-lit,
$1234 ps-push-lit,
' min # call,

$6789 ps-push-lit,
$abcd ps-push-lit,
' min # call,

$aa88 ps-push-lit,
$aa55 ps-push-lit,
' min # call,

$aa22 ps-push-lit,
$aa44 ps-push-lit,
' min # call,

label loop
halt,
loop jr,

[endasm]
