\ Return the greatest power of two that is less or equal to N
\
code irm1b \ isolate rightmost 1-bit
\ based on the formula x & (-x)
\ DE=HL
H D ld,
L E ld,
\ HL=-HL
H ->A-> cpl, A H ld,
L ->A-> cpl, A L ld,
HL inc,
\ HL=HL&DE
H A ld, D A and, A H ld,
L A ld, E A and, A L ld,
end-code

: lo-nibble $0F and ;
: hi-nibble 4 rshift ;
