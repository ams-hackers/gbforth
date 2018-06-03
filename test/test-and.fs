[asm]

main:
$abcd ps-push-lit,
$ff00 ps-push-lit,
' and # call,

$abcd ps-push-lit,
$ff ps-push-lit,
' and # call,

label loop
halt,
loop jr,

[endasm]
