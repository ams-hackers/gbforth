[asm]

main:

$1234 ps-push-lit,
$5678 ps-push-lit,
' = # call,

$5678 ps-push-lit,
$1234 ps-push-lit,
' = # call,

$aa55 ps-push-lit,
$aa88 ps-push-lit,
' = # call,

$aa88 ps-push-lit,
$aa55 ps-push-lit,
' = # call,

$abcd ps-push-lit,
$abcd ps-push-lit,
' = # call,

begin, halt, repeat,

[endasm]
