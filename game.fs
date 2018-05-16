require ./src/asm.fs
require ./src/gbhw.fs

also gb-assembler

' rom, IS emit
' rom-offset IS offset
' rom! IS emit-to

$00 ==> ( restart $00 address )
$08 ==> ( restart $08 address )
$10 ==> ( restart $10 address )
$18 ==> ( restart $18 address )
$20 ==> ( restart $20 address )
$28 ==> ( restart $28 address )
$30 ==> ( restart $30 address )
$38 ==> ( restart $38 address )
$40 ==> reti,   ( vertical blank interrupt start address )
$48 ==> reti,   ( timer overflowInterrupt start address )
$50 ==> reti,   ( LCDC status interrupt start address )
$58 ==> reti,   ( serial transfer completion interrupt start address  )
$60 ==> reti,

( these are written to the memory reserved for high-to-low of p10-p13 interrupt start addresses )
(
;***************************************************************************
;*
;* mem_Set - "Set" a memory region
;*
;* input:
;*    a - value
;*   hl - pMem
;*   bc - bytecount
;*
;***************************************************************************
)
label mem_Set
local
presume .skip
  b inc,
  c inc,
  .skip jr,
label .loop
  a [hl+] ld,
label .skip
  c dec,
  .loop #nz jr,
  b dec,
  .loop #nz jr,
  ret,
end-local


label mem_Copy
local
presume .skip
  b inc,
  c inc,
  .skip jr,
label .loop
  [hl+] a ld,
  a [de] ld,
  de inc,
label .skip
  c dec,
  .loop #nz jr,
  b dec,
  .loop #nz jr,
  ret,
end-local

(
;***************************************************************************
;*
;* mem_Copy - "Copy" a monochrome font from ROM to RAM
;*
;* input:
;*   hl - pSource
;*   de - pDest
;*   bc - bytecount of Source
;*
;*************************************************************************** )
label mem_CopyMono
local
presume .skip
    b inc,
    c inc,
    .skip jr,

label .loop
    [HL+] a ld,
    a [DE] ld,
    de inc,
    a [DE] ld,
    de inc,

label .skip
    c dec,
    .loop #nz jr,
    b dec,
    .loop #nz jr,
    ret,
end-local

: lcd_WaitVRAM
  here<
    [rSTAT] a ld,
    STATF_BUSY # and,
  <there #nz jr, ;


(
;***************************************************************************
;*
;* mem_SetVRAM - "Set" a memory region in VRAM
;*
;* input:
;*    a - value
;*   hl - pMem
;*   bc - bytecount
;*
;***************************************************************************
)
label mem_SetVRam
local
presume .skip
    b inc,
    c inc,
    .skip jr,
label .loop
    af push,
    di,
    lcd_WaitVRAM
    af pop,
    a [hl+] ld,
    ei,

label .skip
    c dec,
    .loop #nz jr,
    b dec,
    .loop #nz jr,
    ret,
end-local

(
;***************************************************************************
;*
;* mem_CopyVRAM - "Copy" a memory region to or from VRAM
;*
;* input:
;*   hl - pSource
;*   de - pDest
;*   bc - bytecount
;*
;***************************************************************************
)
label mem_CopyVRAM
local
presume .skip
  b inc,
  c inc,
  .skip jr,
label .loop
  di,
  lcd_WaitVRAM
  [hl+] a ld,
  a [de] ld,
  ei,
  de inc,
label .skip
  c dec,
  .loop #nz jr,
  b dec,
  .loop #nz jr,
  ret,
end-local

( A placeholder for values)
: $xx $42 ;

$100 ==> ( start entry point )

presume main

nop,
main jp,

( start header )

$0104 ==> ( nintendo logo )
logo
title: EXAMPLE
$143 ==> gbgame

$144 ==>
$00 rom, $00 rom, ( licensee code )
$00 rom,          ( gb 00 or super gameboy 03 )
$00 rom,          ( cartridge type - rom only )
$00 rom,          ( rom size )
$00 rom,          ( ram size )
$01 rom,          ( market code jp/int )
$33 rom,          ( licensee code )
$00 rom,          ( mask rom version number )
$xx rom,          ( complement check )
$f8 rom, $9c rom, ( checksum )

( header end )

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

require ./src/fonts.fs

depth 0<> [if]
  ." The stack is not empty!" cr
[endif]

PREVIOUS
