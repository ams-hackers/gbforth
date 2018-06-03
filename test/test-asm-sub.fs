[asm]

main:

\ [HL] A ld,
$22 # A ld, A $8500 ]* ld,
$33 # A ld,
$8500 # HL ld, [HL] A sub,

A D ld,

\ n A sub,
$33 # A ld,
$22 # A sub,

A E ld,

\ B A sub,
$33 # A ld,
$22 # B ld,
B A sub,

begin, halt, repeat,

[endasm]
