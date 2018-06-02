[asm]


: double dup + ;

main:
$22 ps-push-lit,   \ push $11 to the parameter stack [at $FFFD-$FFFE]
' double # call,   \ call quadruple [dup + dup +]

label loop
halt,
loop jr,

[endasm]
