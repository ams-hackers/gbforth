[asm]

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
  b inc,
  c inc,
  there> jr,
label .loop
  a [hl+] ld,
>here
  c dec,
  .loop #nz jr,
  b dec,
  .loop #nz jr,
  ret,
end-local


label mem_Copy
local
  b inc,
  c inc,
  there> jr,
label .loop
  [hl+] a ld,
  a [de] ld,
  de inc,
>here
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
    b inc,
    c inc,
    there> jr,

label .loop
    [HL+] a ld,
    a [DE] ld,
    de inc,
    a [DE] ld,
    de inc,

>here
    c dec,
    .loop #nz jr,
    b dec,
    .loop #nz jr,
    ret,
end-local

[host]
[asm]

: lcd_WaitVRAM
  begin,
    [rSTAT] a ld,
    STATF_BUSY # A and,
  #z until, ;

[endasm]
[endhost]

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
    b inc,
    c inc,
    there> jr,
label .loop
    af push,
    di,
    lcd_WaitVRAM
    af pop,
    a [hl+] ld,
    ei,

>here
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
  b inc,
  c inc,
  there> jr,
label .loop
  di,
  lcd_WaitVRAM
  [hl+] a ld,
  a [de] ld,
  ei,
  de inc,
>here
  c dec,
  .loop #nz jr,
  b dec,
  .loop #nz jr,
  ret,
end-local

[endasm]
