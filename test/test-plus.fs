[asm]

label main

ps-clear,

$11 ps-push-lit,
$22 ps-push-lit,
' + # call,

label loop
halt,
loop jr,

[endasm]
