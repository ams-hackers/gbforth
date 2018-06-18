require gbhw.fs

\ Read up,down,left,right keys
: P14 $20 rP1 c! ;
\ Read A,B,Start,Pause keys
: P15 $10 rP1 c! ;

\ We have to wait approximately 18 cycles between setting P14-P15 and
\ reading P10-P13. Assuming it is not optimized, CALL/RET should have
\ an overhead of 10 cycles. So we nest 2 empty words here.
: nothing ;
: wait nothing ;

: P10-P13 rP1 c@ invert %1111 and ;

: key-state
  P14 wait P10-P13
  P15 wait P10-P13 4 lshift
  or ;

%00000001 constant k-right
%00000010 constant k-left
%00000100 constant k-up
%00001000 constant k-down
