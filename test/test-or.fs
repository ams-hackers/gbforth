[asm]

main:
$abcd ps-push-lit,
$ff00 ps-push-lit,
' or # call,

$abcd ps-push-lit,
$ff ps-push-lit,
' or # call,

begin, halt, repeat,

[endasm]
