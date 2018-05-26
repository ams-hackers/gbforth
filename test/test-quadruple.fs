also gb-assembler

$150 ==>
label main

( *** Forth kernel demo start *** )
ps-clear,           \ clears the parameter stack [set C to $FE]
$11 ps-push-lit,    \ push $11 to the parameter stack [at $FFFD-$FFFE]
quadruple # call,   \ call quadruple [dup + dup +]

label loop
halt,
loop jr,

previous
