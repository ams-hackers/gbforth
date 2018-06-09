require gbhw.fs

[asm]

title: EXAMPLE

$61 ==>
include ./memory.fs

$150 ==>
__start:

( program start )

di,
$ffff # sp ld,

%11100100 # a ld,

a [rGBP] ld,

0 # a ld,
a [rSCX] ld,
a [rSCY] ld,

there> call, named-ref> >StopLCD



there> hl ld, named-ref> >TileData
_VRAM # de ld,
[host] 256 8 * [endhost] # bc ld,

mem_CopyMono call,

[host]
  LCDCF_ON
  LCDCF_BG8000 or
  LCDCF_BG9800 or
  LCDCF_BGON or
  LCDCF_OBJ16 or
  LCDCF_OBJOFF or
[endhost] # a ld,

a [rLCDC] ld,

#32 # a ld,

_SCRN0 # hl ld,

[host] SCRN_VX_B SCRN_VY_B * [endhost] # bc ld,

mem_SetVRAM call,

[host]
: %Title s" Hello World !" ;
[endhost]

there> hl ld, named-ref> >Title
[host] _SCRN0 3 + SCRN_VY_B 7 * + [endhost] # de ld,

[host] %Title nip [endhost] # bc ld,

mem_CopyVRAM call,

label wait
halt,
nop,
wait jr,

( HACK: Don't use dmgforth internals here )
>Title
[host]
also dmgforth
%title rom-move
previous
[endhost]

nop,

>StopLCD
[rLCDC] A ld,
rlca,

#NC ret,

label .wait
[rLY] A ld,
#145 # A cp,
.wait #NZ jr,
[rLCDC] A ld,
A #7 # res,
A [rLCDC] ld,

ret,

>TileData
include ./ibm-font.fs

[endasm]
