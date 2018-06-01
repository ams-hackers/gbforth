[asm]

label main

ps-init,

$11 ps-push-lit,
$22 ps-push-lit,
$33 ps-push-lit,
' swap # call,

label loop
halt,
loop jr,

[endasm]
