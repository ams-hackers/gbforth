require gbhw.fs
require memory.fs
require cpu.fs

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
  %11100100 rGBP ! ;

: reset-window-scroll
  0 rSCX !
  0 rSCY ! ;

code disable-lcd
[rLCDC] A ld,
rlca,

#NC ret,

here<
[rLY] A ld,
#145 # A cp,
<there #NZ jr,
[rLCDC] A ld,
A #7 # res,
A [rLCDC] ld,

ret,
end-code

: copy-font
  TileData _VRAM [ 256 8 * ]L cmovemono ;

: enable-lcd
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BG9800 or
    LCDCF_BGON or
    LCDCF_OBJ16 or
    LCDCF_OBJOFF or ]L
  rLCDC ! ;

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
