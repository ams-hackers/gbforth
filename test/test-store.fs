[asm]

$150 ==>
label main

ps-clear,

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
