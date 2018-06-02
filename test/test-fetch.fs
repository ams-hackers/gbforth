[asm]

main:

$0104 ps-push-lit, \ ce ed
' @ # call,
$0106 ps-push-lit, \ 66 66
' @ # call,

label loop
halt,
loop jr,

[endasm]
