[asm]

label main

ps-clear,

$66 ps-push-lit,
$ed ps-push-lit,
$ce ps-push-lit,

$8501 ps-push-lit,
' c! # call,
$8502 ps-push-lit,
' c! # call,
$8503 ps-push-lit,
' c! # call,

label loop
halt,
loop jr,

[endasm]
