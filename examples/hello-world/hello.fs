require gbhw.fs
require memory.fs
require cpu.fs
require lcd.fs

require input.fs

title: EXAMPLE
gamecode: HELO
makercode: RX

create TileData
include ibm-font.fs

s" Hello World !"
constant TitleLength
constant TitleOffset

: reset-palette
  %11100100 rGBP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

: copy-font
  TileData _VRAM [ 256 8 * ]L cmovemono ;

: clear-screen
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L bl fill ;

: copy-title
  TitleOffset [ _SCRN0 3 + SCRN_VY_B 7 * + ]L TitleLength cmovevideo ;

( program start )

: rSCX+! rSCX c@ + rSCX c! ;
: rSCY+! rSCY c@ + rSCY c! ;

: handle-input
  begin
    rDIV c@ [ $FF 8 / ]L < if
      key-state
      dup k-right and if -1 rSCX+! then
      dup k-left  and if  1 rSCX+! then
      dup k-up    and if  1 rSCY+! then
      dup k-down  and if -1 rSCY+! then
      drop
    then
  again ;

: main
  disable-interrupts
  reset-palette
  reset-window-scroll
  disable-lcd copy-font enable-lcd
  clear-screen
  copy-title
  handle-input ;
