[asm]

main:

$1234 ps-push-lit,
$5676 ps-push-lit,
' 1+ # call,
' 1+ # call,

label loop
halt,
loop jr,

[endasm]
