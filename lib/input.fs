require ./gbhw.fs
require ./bits.fs
require ./cpu.fs

\ Read up,down,left,right keys
: P15 P1F_5 rP1 c! ;
\ Read A,B,Start,Pause keys
: P14 P1F_4 rP1 c! ;
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

\ Wait until a key is pressed and return
: key
  begin
    \ Use irm1b to isolate the rightmost 1-bit set, disambiguating in
    \ case multiple keys are press
    halt key-state irm1b
  ?dup until ;

PADF_A      constant k-a
PADF_B      constant k-b
PADF_SELECT constant k-select
PADF_START  constant k-start
PADF_RIGHT  constant k-right
PADF_LEFT   constant k-left
PADF_UP     constant k-up
PADF_DOWN   constant k-down

: enable-interrupt-flags ( u -- )
  0 rIF c!
  rIE c@ or rIE c! ;

: init-input
  IEF_HILO enable-interrupt-flags ;
