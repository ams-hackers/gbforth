[asm]


: f 10 ;
: f 1 f + ;

main:
' f # call,   \ call quadruple [dup + dup +]

begin, halt, repeat,

[endasm]
