[asm]

label main

ps-init,

$1234 ps-push-lit,
$abcd ps-push-lit,

$8501 ps-push-lit,
' ! # call,
$8503 ps-push-lit,
' ! # call,

label loop
halt,
loop jr,

[endasm]
