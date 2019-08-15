require ./cpu.fs
require ./gbhw.fs
require ./memory.fs
require ./formatted-output.fs

variable cursor-x
variable cursor-y

: cursor-addr ( -- addr )
  _SCRN0 cursor-x @ +
  SCRN_VY_B cursor-y @ *
  + ;

: at-xy ( x y -- )
  cursor-y !
  cursor-x ! ;

\ Clear the screen
: page
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L blank
  0 0 at-xy ;

: form ( -- h w ) SCRN_Y_B SCRN_X_B ;

: cr
  0 cursor-x !
  cursor-y @ 1 + SCRN_VY_B mod cursor-y ! ;

: emit
  dup 10 = if
    cr
  else
    ( n ) cursor-addr c!video
    1 cursor-x +!
  then ;

: space bl emit ;
: spaces 0 ?do space loop ;

: type ( addr u -- )
  tuck
  cursor-addr swap cmovevideo
  cursor-x +! ;

: typewhite nip spaces ;

: . ( n -- )
  dup 0< if
    [char] - emit
    0 swap -
  then
  <# #s #> type space ;

: ? c@ . ;

:m ."
  postpone s"
  postpone type
; immediate

: .r ( n1 n2 -- )
  swap <# #s #>
  ( width addr u )
  rot over - 0 max spaces
  type ;

: reset-palette
  %11100100 rBGP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

: init-term
  disable-interrupts
  reset-palette
  reset-window-scroll
  enable-interrupts
  page
;
