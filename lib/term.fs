require ./cpu.fs
require ./gbhw.fs
require ./memory.fs

create TileData
include ./ibm-font.fs

: copy-font
  TileData _VRAM [ 256 8 * ]L cmovemono ;

: page
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L bl fill ;

: type ( addr u -- )
  [ _SCRN0 3 + SCRN_VY_B 7 * + ]L swap cmovevideo ;

: reset-palette
  %11100100 rGBP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

:m ."
  postpone s"
  postpone type
; immediate

: init-term
  disable-interrupts
  reset-palette
  reset-window-scroll

  disable-lcd
  copy-font
  enable-lcd
  enable-interrupts
;
