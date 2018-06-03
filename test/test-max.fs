[asm]

main:

$1234 ps-push-lit,
$5678 ps-push-lit,
' max # call,

$abcd ps-push-lit,
$6789 ps-push-lit,
' max # call,

$aa55 ps-push-lit,
$aa88 ps-push-lit,
' max # call,

$aa44 ps-push-lit,
$aa22 ps-push-lit,
' max # call,

label loop
halt,
loop jr,

[endasm]
