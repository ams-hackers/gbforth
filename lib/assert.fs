require gbhw.fs

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

code disable-lcd
[rLCDC] A ld,
rlca,

#NC ret,

here<
[rLY] A ld,
#140 # A cp,
<there #NZ jr,
[rLCDC] A ld,
A #7 # res,
A [rLCDC] ld,

ret,
end-code

[asm]
\ label TileData
offset constant TileData ( HACK: label is ASM-only )

[host]
also gbforth

: >
  parse-name
  dup 8 <> if true abort" The tile line length is wrong!" then
  0 swap
  0 ?do
    1 lshift
    over i + c@ case
      [char] X of 1 or endof
      [char] . of      endof
      true abort" Wrong character!"
    endcase
  loop
  nip
  rom, ;

previous
[endhost]

> ........
> ........
> ........
> ........
> ........
> ........
> ........
> ........

> .XXXXXX.
> XXXXXXXX
> XX.XX.XX
> XXXXXXXX
> XXX..XXX
> XX....XX
> XXXXXXXX
> .XXXXXX.

> .XXXXXX.
> X......X
> X.X..X.X
> X......X
> X.XXXX.X
> X..XX..X
> X......X
> .XXXXXX.

3 constant TileCount

[endasm]

: copy-font
  TileData _VRAM [ TileCount 8 * ]L cmovemono ;

: enable-lcd
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BG9800 or
    LCDCF_BGON or
    LCDCF_OBJ16 or
    LCDCF_OBJOFF or ]L
  rLCDC ! ;

: clear-screen
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L 0 fill ;

#9 constant display-x
#8 constant display-y

: display-face ( f -- )
  1 and 1 + ( false: 1, true: 2 )
  [ _SCRN0 display-x + SCRN_VY_B display-y * + ]L
  c! ;

: assert
  disable-lcd
  copy-font clear-screen display-face
  enable-lcd ;
