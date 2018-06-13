[asm]

main:

$1234 push-lit,
$5678 push-lit,
' = # call,

$5678 push-lit,
$1234 push-lit,
' = # call,

$aa55 push-lit,
$aa88 push-lit,
' = # call,

$aa88 push-lit,
$aa55 push-lit,
' = # call,

$abcd push-lit,
$abcd push-lit,
' = # call,

begin, halt, repeat,

[endasm]
