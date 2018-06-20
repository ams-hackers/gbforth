( port/mode registers )
$FF00 constant rP1      ( Register for reading joy pad info [R/W] )
$FF01 constant rSB      ( Serial Transfer Data [R/W] )
$FF02 constant rSC      ( Serial I/O Control [R/W] )

$FF04 constant rDIV     ( Divider register [R/W] )
$FF05 constant rTIMA    ( Timer counter [R/W] )
$FF06 constant rTMA     ( Timer modulo [R/W] )
$FF07 constant rTAC     ( Timer control [R/W] )

$FF4D constant rKEY1    ( [CGB only] CPU speed switching [R/W] )
$FF56 constant rRP      ( [CGB only] Infrared communication port [R/W] )

( Bank control registers )
$FF4F constant rVBK     ( [CGB only] VRAM bank specification [R/W] )
$FF70 constant rSVBK    ( [CGB only] WRAM Bank specification [R/W] )

( Interrupt flags )
$FF0F constant rIF      ( Interrupt Flag [R/W] )
$FFFF constant rIE      ( Interrupt Enable [R/W] )

( Sound channel 1 registers )
$FF10 constant rNR10    ( Sweep register [R/W] )
$FF11 constant rNR11    ( Sound length / wave pattern duty [R/W] )
$FF12 constant rNR12    ( Envelope [R/W] )
$FF13 constant rNR13    ( Frequency lo [W] )
$FF14 constant rNR14    ( Frequency hi [W] )

( Sound channel 2 registers )
$FF16 constant rNR21    ( Sound length / wave pattern duty [R/W] )
$FF17 constant rNR22    ( Envelope [R/W] )
$FF18 constant rNR23    ( Frequency lo [W] )
$FF19 constant rNR24    ( Frequency hi [W] )

( Sound channel 3 registers )
$FF1A constant rNR30    ( Sound on/off [R/W] )
$FF1B constant rNR31    ( Sound length [R/W] )
$FF1C constant rNR32    ( Select output level )
$FF1D constant rNR33    ( Frequency lo [W] )
$FF1E constant rNR34    ( Frequency hi [W] )

( Sound channel 4 registers )
$FF20 constant rNR41    ( Sound length [R/W] )
$FF21 constant rNR42    ( Envelope [R/W] )
$FF22 constant rNR43    ( Polynomial counter [W] )
$FF23 constant rNR44    ( Initialise/length [R/W] )

( Sound control registers )
$FF24 constant rNR50    ( Channel control / ON-OFF / Volume [R/W] )
$FF25 constant rNR51    ( Selection of sound output terminal [R/W] )
$FF26 constant rNR52    ( Sound on/off [R/W] )

( Alternative sound register names )
rNR10 constant rAUD1SWEEP
rNR11 constant rAUD1LEN
rNR12 constant rAUD1ENV
rNR13 constant rAUD1LOW
rNR14 constant rAUD1HIGH
rNR21 constant rAUD2LEN
rNR22 constant rAUD2ENV
rNR23 constant rAUD2LOW
rNR24 constant rAUD2HIGH
rNR30 constant rAUD3ENA
rNR31 constant rAUD3LEN
rNR32 constant rAUD3LEVEL
rNR33 constant rAUD3LOW
rNR34 constant rAUD3HIGH
rNR41 constant rAUD4LEN
rNR42 constant rAUD4ENV
rNR43 constant rAUD4POLY
rNR44 constant rAUD4GO
rNR50 constant rAUDVOL
rNR51 constant rAUDTERM
rNR52 constant rAUDENA

( LCD display registers )
$FF40 constant rLCDC    ( LCD control [R/W] )
$FF41 constant rSTAT    ( LCD status [R/W] )
$FF42 constant rSCY     ( Scroll Y [R/W] )
$FF43 constant rSCX     ( Scroll X [R/W] )
$FF44 constant rLY      ( LCDC Y-Coordinate [144->153 is VBlank period] [R] )
$FF45 constant rLYC     ( LY Compare [R/W] )
$FF46 constant rDMA     ( DMA Transfer and Start Address [W] )
$FF47 constant rGBP     ( BG Palette Data [W] )
$FF48 constant rOBP0    ( Object Palette 0 Data [W] )
$FF49 constant rOBP1    ( Object Palette 1 Data [W] )
$FF4A constant rWY      ( Window Y Position [R/W] )
$FF4B constant rWX      ( Window X Position [R/W] )

$FF51 constant rHDMA1   ( [CGB only] Higher-order address of HDMAtransfer source [W] )
$FF52 constant rHDMA2   ( [CGB only] Lower-order address of HDMAtransfer source [W] )
$FF53 constant rHDMA3   ( [CGB only] Higher-order address of HDMAtransfer destination [W] )
$FF54 constant rHDMA4   ( [CGB only] Lower-order address of HDMAtransfer destination [W] )
$FF55 constant rHDMA5   ( [CGB only] H-blank and general-purpose HDMA control [W] )

$FF68 constant rBCPS    ( [CGB only] Color palette BG write specification [R/W] )
$FF69 constant rGCPD    ( [CGB only] Color palette BG write data [R/W] )
$FF6A constant rOCPS    ( [CGB only] Color palette OBJ write specification [R/W] )
$FF6B constant rOCPD    ( [CGB only] Color palette OBJ write data [R/W] )

( Register references to use in ASM )
[host]
[asm]

( port/mode register references )
: [rP1] rP1 ]* ;
: [rSB] rSB ]* ;
: [rSC] rSC ]* ;

: [rDIV] rDIV ]* ;
: [rTIMA] rTIMA ]* ;
: [rTMA] rTMA ]* ;
: [rTAC] rTAC ]* ;

: [rKEY1] rKEY1 ]* ;
: [rRP] rRP ]* ;

( Bank control register references )
: [rVBK] rVBK ]* ;
: [rSVBK] rSVBK ]* ;

( Interrupt flag references )
: [rIF] rIF ]* ;
: [rIE] rIE ]* ;

( Sound channel 1 register refereces )
: [rNR10] rNR10 ]* ;
: [rNR11] rNR11 ]* ;
: [rNR12] rNR12 ]* ;
: [rNR13] rNR13 ]* ;
: [rNR14] rNR14 ]* ;

( Sound channel 2 register refereces )
: [rNR21] rNR21 ]* ;
: [rNR22] rNR22 ]* ;
: [rNR23] rNR23 ]* ;
: [rNR24] rNR24 ]* ;

( Sound channel 3 register refereces )
: [rNR30] rNR30 ]* ;
: [rNR31] rNR31 ]* ;
: [rNR32] rNR32 ]* ;
: [rNR33] rNR33 ]* ;
: [rNR34] rNR34 ]* ;

( Sound channel 4 register refereces )
: [rNR41] rNR41 ]* ;
: [rNR42] rNR42 ]* ;
: [rNR43] rNR43 ]* ;
: [rNR44] rNR44 ]* ;

( Sound control register refereces )
: [rNR50] rNR50 ]* ;
: [rNR51] rNR51 ]* ;
: [rNR52] rNR52 ]* ;

( Alternative sound register refereces )
: [rAUD1SWEEP] [rNR10] ;
: [rAUD1LEN]   [rNR11] ;
: [rAUD1ENV]   [rNR12] ;
: [rAUD1LOW]   [rNR13] ;
: [rAUD1HIGH]  [rNR14] ;
: [rAUD2LEN]   [rNR21] ;
: [rAUD2ENV]   [rNR22] ;
: [rAUD2LOW]   [rNR23] ;
: [rAUD2HIGH]  [rNR24] ;
: [rAUD3ENA]   [rNR30] ;
: [rAUD3LEN]   [rNR31] ;
: [rAUD3LEVEL] [rNR32] ;
: [rAUD3LOW]   [rNR33] ;
: [rAUD3HIGH]  [rNR34] ;
: [rAUD4LEN]   [rNR41] ;
: [rAUD4ENV]   [rNR42] ;
: [rAUD4POLY]  [rNR43] ;
: [rAUD4GO]    [rNR44] ;
: [rAUDVOL]    [rNR50] ;
: [rAUDTERM]   [rNR51] ;
: [rAUDENA]    [rNR52] ;

( LCD display registers)
: [rLCDC] rLCDC ]* ;
: [rSTAT] rSTAT ]* ;
: [rSCY] rSCY ]* ;
: [rSCX] rSCX ]* ;
: [rLY] rLY ]* ;
: [rLYC] rLYC ]* ;
: [rDMA] rDMA ]* ;
: [rGBP] rGBP ]* ;
: [rOBP0] rOBP0 ]* ;
: [rOBP1] rOBP1 ]* ;
: [rWY] rWY ]* ;
: [rWX] rWX ]* ;

: [rHDMA1] rHDMA1 ]* ;
: [rHDMA2] rHDMA2 ]* ;
: [rHDMA3] rHDMA3 ]* ;
: [rHDMA4] rHDMA4 ]* ;
: [rHDMA5] rHDMA5 ]* ;

: [rBCPS] rBCPS ]* ;
: [rGCPD] rGCPD ]* ;
: [rOCPS] rOCPS ]* ;
: [rOCPD] rOCPD ]* ;

[endasm]
[target]

( Memory addresses )
$0150 constant _ROM          ( $0150->$7FFF )
\ $0150 constant _ROM0       ( -> $0150->$3FFF )
\ $4FFF constant _ROMX       ( -> $4000->$7FFF )
$8000 constant _VRAM         ( $8000->$9FFF )
\ $8000 constant _TILERAM    ( -> $8000->$97FF )
$9800 constant _SCRN0        ( -> $9800->$9BFF )
$9C00 constant _SCRN1        ( -> $9C00->$9FFF )
$A000 constant _CARTRAM        ( $A000->$BFFF )
$C000 constant _WRAM         ( $C000->$DFFF )
\ $C000 constant _WRAM0      ( -> $C000->$CFFF )
\ $D000 constant _WRAMX      ( -> $D000->$DFFF )
\ $E000 constant _ECHORAM    ( $E000->$FDFF )
$FE00 constant _OAM          ( $FE00->$FE9F )
$FF00 constant _HW           ( $FF00->$FF7F )
$FF30 constant _AUD3WAVERAM  ( -> $FF30->$FF3F )
$FF80 constant _HRAM         ( $FF80->$FFFE )

( OAM flags )
%10000000 constant OAMF_PRI    ( Priority )
%01000000 constant OAMF_YFLIP  ( Y flip )
%00100000 constant OAMF_XFLIP  ( X flip )
%00000000 constant OAMF_PAL0   ( Palette number; 0,1 )
%00010000 constant OAMF_PAL1   ( Palette number; 0,1 )

( P1 joy pad flags )
%00100000 constant P1F_5   ( P15 out port )
%00010000 constant P1F_4   ( P14 out port )
%00001000 constant P1F_3   ( P13 in port )
%00000100 constant P1F_2   ( P12 in port )
%00000010 constant P1F_1   ( P11 in port )
%00000001 constant P1F_0   ( P10 in port )

( LCD control flags )
%00000000 constant LCDCF_OFF        ( LCD Control Operation)
%10000000 constant LCDCF_ON         ( LCD Control Operation)
%00000000 constant LCDCF_WIN9800    ( Window Tile Map Display Select)
%01000000 constant LCDCF_WIN9C00    ( Window Tile Map Display Select)
%00000000 constant LCDCF_WINOFF     ( Window Display)
%00100000 constant LCDCF_WINON      ( Window Display)
%00000000 constant LCDCF_BG8800     ( BG & Window Tile Data Select)
%00010000 constant LCDCF_BG8000     ( BG & Window Tile Data Select)
%00000000 constant LCDCF_BG9800     ( BG Tile Map Display Select)
%00001000 constant LCDCF_BG9C00     ( BG Tile Map Display Select)
%00000000 constant LCDCF_OBJ8       ( OBJ Construction)
%00000100 constant LCDCF_OBJ16      ( OBJ Construction)
%00000000 constant LCDCF_OBJOFF     ( OBJ Display)
%00000010 constant LCDCF_OBJON      ( OBJ Display)
%00000000 constant LCDCF_BGOFF      ( BG Display)
%00000001 constant LCDCF_BGON       ( BG Display)

( LCD status flags )
%01000000 constant STATF_LYC     ( LYCEQULY Coincidence [Selectable])
%00100000 constant STATF_MODE10  ( Mode 10)
%00010000 constant STATF_MODE01  ( Mode 01 [V-Blank])
%00001000 constant STATF_MODE00  ( Mode 00 [H-Blank])
%00000100 constant STATF_LYCF    ( Coincidence Flag)
%00000000 constant STATF_HB      ( H-Blank)
%00000001 constant STATF_VB      ( V-Blank)
%00000010 constant STATF_OAM     ( OAM-RAM is used by system)
%00000011 constant STATF_LCD     ( Both OAM and VRAM used by system)
%00000010 constant STATF_BUSY    ( When set, VRAM access is unsafe )

( Timer control flags )
%00000100 constant TACF_START
%00000000 constant TACF_STOP
%00000000 constant TACF_4KHZ
%00000011 constant TACF_16KHZ
%00000010 constant TACF_65KHZ
%00000001 constant TACF_262KHZ

( Interupt enable flags )
%00010000 constant IEF_HILO    ( Transition from High to Low of Pin number P10-P13 )
%00001000 constant IEF_SERIAL  ( Serial I/O transfer end )
%00000100 constant IEF_TIMER   ( Timer Overflow )
%00000010 constant IEF_LCDC    ( LCDC [see STAT] )
%00000001 constant IEF_VBLANK  ( V-Blank )

(
  *************************
  Keypad related
  *************************
)

$80 constant PADF_DOWN
$40 constant PADF_UP
$20 constant PADF_LEFT
$10 constant PADF_RIGHT
$08 constant PADF_START
$04 constant PADF_SELECT
$02 constant PADF_B
$01 constant PADF_A

$7 constant PADB_DOWN
$6 constant PADB_UP
$5 constant PADB_LEFT
$4 constant PADB_RIGHT
$3 constant PADB_START
$2 constant PADB_SELECT
$1 constant PADB_B
$0 constant PADB_A

(
  *************************
  Screen related
  *************************
)

#160 constant SCRN_X    ( Width of screen in pixels )
#144 constant SCRN_Y    ( Height of screen in pixels )
#20  constant SCRN_X_B  ( Width of screen in bytes )
#18  constant SCRN_Y_B  ( Height of screen in bytes )

#256 constant SCRN_VX   ( Virtual width of screen in pixels )
#256 constant SCRN_VY   ( Virtual height of screen in pixels )
#32  constant SCRN_VX_B ( Virtual width of screen in bytes )
#32  constant SCRN_VY_B ( Virtual height of screen in bytes )
