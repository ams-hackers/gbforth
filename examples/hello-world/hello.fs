require gbhw.fs

title: EXAMPLE
gamecode: HELO
makercode: RX

code cmovemono ( c-from c-to u -- )
[C] ->A-> E ld,  C inc,
[C] ->A-> D ld,  C inc,

C inc, BC push,
[C] ->A-> B ld,  C dec,
[C] ->A-> C ld,

begin, H|L->A, #NZ while,
  [BC] ->A-> [DE] ld,
  DE inc,
  [BC] ->A-> [DE] ld, \ duplicate copied byte
  BC inc,
  DE inc,

  HL dec,
repeat,

BC pop, C inc,
ps-drop,
end-code

code cmovevideo ( c-from c-to u -- )
[C] ->A-> E ld,  C inc,
[C] ->A-> D ld,  C inc,

C inc, BC push,
[C] ->A-> B ld,  C dec,
[C] ->A-> C ld,

begin, H|L->A, #NZ while,
  di,
  lcd_WaitVRAM          \ cmove but with di, waitvram, ei,
  [BC] ->A-> [DE] ld,
  ei,
  BC inc,
  DE inc,

  HL dec,
repeat,

BC pop, C inc,
ps-drop,
end-code

[asm]

\ label TileData
offset constant TileData ( HACK: label is ASM-only )
include ibm-font.fs

[host]
: %Title s" Hello World !" ;
[endhost]
[host] %Title nip [endhost] constant TitleLength

\ label Title
offset constant Title ( HACK: label is ASM-only )
[host]
also gbforth ( HACK: Don't use gbforth internals here )
%title rom-move
previous
[endhost]

[endasm]

code disable-interrupts
di,
ret,
end-code

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
  TileData _VRAM 256 8 * cmovemono ;

: enable-lcd
  LCDCF_ON
  LCDCF_BG8000 or
  LCDCF_BG9800 or
  LCDCF_BGON or
  LCDCF_OBJ16 or
  LCDCF_OBJOFF or
  rLCDC ! ;

: clear-screen
  _SCRN0 SCRN_VX_B SCRN_VY_B * bl fill ;

: copy-title
  Title
  _SCRN0 3 + SCRN_VY_B 7 * +
  TitleLength
  cmovevideo ;

( program start )

: main
  disable-interrupts
  reset-palette
  reset-window-scroll
  disable-lcd copy-font enable-lcd
  clear-screen
  copy-title ;
