also gb-assembler

$150 ==>
label main

ps-clear,

\ [HL] A add,
$11 # A ld, A $8500 ]* ld,
$22 # A ld,
$8500 # HL ld, [HL] A add,

A D ld,

\ n A add,
$11 # A ld,
$22 # A add,

A E ld,

\ B A add,
$11 # A ld,
$22 # B ld,
B A add,

label loop
halt,
loop jr,

previous
