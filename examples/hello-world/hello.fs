require gbhw.fs

title: EXAMPLE
gamecode: HELO
makercode: RX

[asm]

include memory.fs
label TileData
include ibm-font.fs

label StopLCD
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

[host]
: %Title s" Hello World !" ;
[endhost]

( HACK: Don't use dmgforth internals here )
label Title
[host]
also dmgforth
%title rom-move
previous
[endhost]
ret,

: clear-screen
  _SCRN0 SCRN_VX_B SCRN_VY_B * bl fill ;
( Force clear-screen to be emitted now )
' clear-screen drop

label start

( program start )

di,

%11100100 # a ld,

a [rGBP] ld,

0 # a ld,
a [rSCX] ld,
a [rSCY] ld,

StopLCD call,

TileData hl ld,
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

' clear-screen # call,

Title hl ld,
[host] _SCRN0 3 + SCRN_VY_B 7 * + [endhost] # de ld,

[host] %Title nip [endhost] # bc ld,

mem_CopyVRAM call,

label wait
halt,
nop,
wait jr,

nop,

[endasm]

code main
  start call,
end-code
