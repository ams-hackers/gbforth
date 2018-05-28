also gb-assembler

$150 ==>

: f 10 ;
: f 1 f + ;

label main
( *** Forth kernel demo start *** )
ps-clear,           \ clears the parameter stack [set C to $FE]
' f # call,   \ call quadruple [dup + dup +]

label loop
halt,
loop jr,

previous
