[asm]

main:

$1234 ps-push-lit,
$5678 ps-push-lit,
' > # call,

$abcd ps-push-lit,
$6789 ps-push-lit,
' > # call,

$aa55 ps-push-lit,
$aa88 ps-push-lit,
' > # call,

$aa44 ps-push-lit,
$aa22 ps-push-lit,
' > # call,

label loop
halt,
loop jr,

[endasm]
