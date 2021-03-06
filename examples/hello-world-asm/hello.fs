title: EXAMPLE

require gbhw.fs
ROM

[asm]

$61 ==>
include ./memory.fs

$150 ==>
main:

( program start )

di,
$ffff # sp ld,

%11100100 # a ld,

a [rBGP] ld,

0 # a ld,
a [rSCX] ld,
a [rSCY] ld,

there> call, named-ref> >StopLCD

there> hl ld, named-ref> >TileData
_VRAM # de ld,
256 8 * # bc ld,

mem_CopyMono call,

LCDCF_ON
LCDCF_BG8000 or
LCDCF_BG9800 or
LCDCF_BGON or
LCDCF_OBJ16 or
LCDCF_OBJOFF or
# a ld,

a [rLCDC] ld,

#32 # a ld,

_SCRN0 # hl ld,

SCRN_VX_B SCRN_VY_B * # bc ld,

mem_SetVRAM call,

[host]
s" Hello World !" 2CONSTANT %Title
[target]

there> hl ld, named-ref> >Title
_SCRN0 3 + SCRN_VY_B 7 * + # de ld,

%Title nip # bc ld,

mem_CopyVRAM call,

here<
halt,
nop,
<there jr,

>Title
%Title mem,

nop,

>StopLCD
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

>TileData
include ./ibm-font.fs

[endasm]
