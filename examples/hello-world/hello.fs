require gbhw.fs
require memory.fs
require cpu.fs
require lcd.fs

title: EXAMPLE
gamecode: HELO
makercode: RX

[asm]
\ label TileData
offset constant TileData ( HACK: label is ASM-only )
include ibm-font.fs
[endasm]

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

: main
  disable-interrupts
  reset-palette
  reset-window-scroll
  disable-lcd copy-font enable-lcd
  clear-screen copy-title ;
