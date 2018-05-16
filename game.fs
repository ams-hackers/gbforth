require ./src/asm.fs
require ./src/gbhw.fs

also gb-assembler

$61 ==> 
include ./lib/memory.fs

$150 ==>
label main

( program start )

di,
$ffff # sp ld,

%11100100 # a ld,

a [rGBP] ld,

0 # a ld,
a [rSCX] ld,
a [rSCY] ld,

presume StopLCD

StopLCD call,


: TileData $01ac # ;

TileData hl ld,
_VRAM # de ld,
256 8 * # bc ld,

mem_CopyMono call,

LCDCF_ON
LCDCF_BG8000 or
LCDCF_BG9800 or
LCDCF_BGON or
LCDCF_OBJ16 or
LCDCF_OBJOFF or # a ld,

a [rLCDC] ld,

#32 # a ld,

_SCRN0 # hl ld,

SCRN_VX_B SCRN_VY_B * # bc ld,

mem_SetVRAM call,

: %Title s" Hello World !" ;
PRESUME Title

Title hl ld,
_SCRN0 3 + SCRN_VY_B 7 * + # de ld,

%Title nip # bc ld,

mem_CopyVRAM call,

label wait
halt,
nop,
wait jr,

label Title
%title rom-move


nop,

label StopLCD
[rLCDC] A ld,
rlca,

#NC ret,

label .wait
[rLY] A ld,
#145 # cp,
.wait #NZ jr,
[rLCDC] A ld,
A #7 # res,
A [rLCDC] ld,

ret,

include ./lib/ibm-font.fs

previous
