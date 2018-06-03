[asm]

main:
$1234 ps-push-lit,
$5678 ps-push-lit,
' 2* # call,

label loop
halt,
loop jr,

[endasm]
