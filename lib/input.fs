require gbhw.fs

\ Read up,down,left,right keys
: P14 $20 rP1 c! ;
\ Read A,B,Start,Pause keys
: P15 $10 rP1 c! ;
\ Reset the P14 and P15 lines
: reset-P14-15
  $00 rP1 c! ;

\ We have to wait approximately 18 cycles between setting P14-P15 and
\ reading P10-P13. Assuming it is not optimized, CALL/RET should have
\ an overhead of 10 cycles. So we nest 2 empty words here.
: nothing ;
: wait nothing ;

: P10-P13 rP1 c@ invert %1111 and ;

: key-state
  P14 wait P10-P13
  P15 wait P10-P13 4 lshift or
  \ Ensure that any key will trigger an interruption, so it will
  \ resume the execution after HALT.
  reset-P14-15 ;

%00000001 constant k-right
%00000010 constant k-left
%00000100 constant k-up
%00001000 constant k-down
%00010000 constant k-a
%00100000 constant k-b
%01000000 constant k-select
%10000000 constant k-start

: enable-interrupt-flags ( u -- )
  0 rIF c!
  rIE c@ or rIE c! ;

: init-input
  IEF_HILO enable-interrupt-flags ;
