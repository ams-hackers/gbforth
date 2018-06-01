[asm]

main:

ps-init,

$c7 ps-push-lit,
$df ps-push-lit,
' * # call,

label loop
halt,
loop jr,

[endasm]
