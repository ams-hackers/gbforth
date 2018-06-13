[asm]

main:

$abcd push-lit,
$1234 push-lit,
' - # call,

$89ab push-lit,
$89ab push-lit,
' - # call,

$AAAA push-lit,
$AAAB push-lit,
' - # call,

begin, halt, repeat,

[endasm]
