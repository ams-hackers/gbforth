[asm]

label main

ps-clear,

$c7 ps-push-lit,
$df ps-push-lit,
' * # call,

label loop
halt,
loop jr,

[endasm]
