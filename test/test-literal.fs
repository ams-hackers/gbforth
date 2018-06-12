[asm]

: test $1 [ $2 $3 ] literal literal + + [ $4 ]L + ;

main:
' test # call,

begin, halt, repeat,

[endasm]
