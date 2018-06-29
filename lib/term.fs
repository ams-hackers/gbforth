require ./cpu.fs
require ./gbhw.fs
require ./memory.fs
require ./input.fs
require ./bits.fs
require ./debug.fs

ROM
create TileData
include ./ibm-font.fs
RAM

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

: cr
  0 cursor-x !
  1 cursor-y +! ;

: type ( addr u -- )
  cursor-addr swap cmovevideo ;

:m ."
  postpone s"
  postpone type
; immediate


\ Wait until a key is pressed and return
: key
  begin
    \ Use irm1b to isolate the rightmost 1-bit set, disambiguating in
    \ case multiple keys are press
    halt key-state irm1b
    dup emit
  ?dup until ;

: reset-palette
  %11100100 rGBP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

: init-term
  disable-interrupts
  reset-palette
  reset-window-scroll
  init-input
  disable-lcd
  copy-font
  enable-lcd
  enable-interrupts
;
