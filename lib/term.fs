require ./cpu.fs
require ./gbhw.fs
require ./memory.fs

create TileData
include ./ibm-font.fs

variable cursor-x
variable cursor-y

: cursor-addr ( -- addr )
  _SCRN0 cursor-x @ +
  SCRN_VY_B cursor-y @ *
  + ;

: copy-font
  TileData _VRAM [ 256 8 * ]L cmovemono ;

: at-xy ( x y -- )
  cursor-y !
  cursor-x ! ;

\ Clear the screen
: page
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L bl fill
  0 0 at-xy ;

: type ( addr u -- )
  cursor-addr swap cmovevideo ;

:m ."
  postpone s"
  postpone type
; immediate


: reset-palette
  %11100100 rGBP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

: init-term
  disable-interrupts
  reset-palette
  reset-window-scroll

  disable-lcd
  copy-font
  enable-lcd
  enable-interrupts
;
