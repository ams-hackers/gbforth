SECTION "Memory1 Code",HOME

INCLUDE "gbhw.inc"

; Macro that pauses until VRAM available.

lcd_WaitVRAM: MACRO
        ld      a,[rSTAT]       ; <---+
        and     STATF_BUSY      ;     |
        jr      nz,@-4          ; ----+
        ENDM

        PUSHS           ; Push the current section onto assember stack.

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
mem_Set::
	inc	b
	inc	c
	jr	.skip
.loop	ld	[hl+],a
.skip	dec	c
	jr	nz,.loop
	dec	b
	jr	nz,.loop
	ret

;***************************************************************************
;*
;* mem_Copy - "Copy" a memory region
;*
;* input:
;*   hl - pSource
;*   de - pDest
;*   bc - bytecount
;*
;***************************************************************************
mem_Copy::
	inc	b
	inc	c
	jr	.skip
.loop	ld	a,[hl+]
	ld	[de],a
	inc	de
.skip	dec	c
	jr	nz,.loop
	dec	b
	jr	nz,.loop
	ret

;***************************************************************************
;*
;* mem_Copy - "Copy" a monochrome font from ROM to RAM
;*
;* input:
;*   hl - pSource
;*   de - pDest
;*   bc - bytecount of Source
;*
;***************************************************************************
mem_CopyMono::
	inc	b
	inc	c
	jr	.skip
.loop	ld	a,[hl+]
	ld	[de],a
	inc	de
        ld      [de],a
        inc     de
.skip	dec	c
	jr	nz,.loop
	dec	b
	jr	nz,.loop
	ret


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
mem_SetVRAM::
	inc	b
	inc	c
	jr	.skip
.loop   push    af
        di
        lcd_WaitVRAM
        pop     af
        ld      [hl+],a
        ei
.skip	dec	c
	jr	nz,.loop
	dec	b
	jr	nz,.loop
	ret

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
mem_CopyVRAM::
	inc	b
	inc	c
	jr	.skip
.loop   di
        lcd_WaitVRAM
        ld      a,[hl+]
	ld	[de],a
        ei
	inc	de
.skip	dec	c
	jr	nz,.loop
	dec	b
	jr	nz,.loop
	ret

        POPS           ; Pop the current section off of assember stack.
