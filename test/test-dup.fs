[asm]

main:

ps-init,

$11 ps-push-lit,
$22 ps-push-lit,

' dup # call,

label loop
halt,
loop jr,

[endasm]
