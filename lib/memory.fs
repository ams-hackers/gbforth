require video.fs

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
  b inc,
  c inc,
  there> jr,
label .mem_Set_loop
  a [hl+] ld,
>here
  c dec,
  .mem_Set_loop #nz jr,
  b dec,
  .mem_Set_loop #nz jr,
  ret,


label mem_Copy
  b inc,
  c inc,
  there> jr,
label .mem_Copy_loop
  [hl+] a ld,
  a [de] ld,
  de inc,
>here
  c dec,
  .mem_Copy_loop #nz jr,
  b dec,
  .mem_Copy_loop #nz jr,
  ret,

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
    b inc,
    c inc,
    there> jr,

label .mem_CopyMono_loop
    [HL+] a ld,
    a [DE] ld,
    de inc,
    a [DE] ld,
    de inc,

>here
    c dec,
    .mem_CopyMono_loop #nz jr,
    b dec,
    .mem_CopyMono_loop #nz jr,
    ret,

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
    b inc,
    c inc,
    there> jr,
label .mem_SetVRam_loop
    af push,
    di,
    lcd_WaitVRAM
    af pop,
    a [hl+] ld,
    ei,

>here
    c dec,
    .mem_SetVRam_loop #nz jr,
    b dec,
    .mem_SetVRam_loop #nz jr,
    ret,

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
  b inc,
  c inc,
  there> jr,
label .mem_CopyVRAM_loop
  di,
  lcd_WaitVRAM
  [hl+] a ld,
  a [de] ld,
  ei,
  de inc,
>here
  c dec,
  .mem_CopyVRAM_loop #nz jr,
  b dec,
  .mem_CopyVRAM_loop #nz jr,
  ret,

[endasm]
