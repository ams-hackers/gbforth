[asm]

[host]

( port/mode registers )
$FF00 constant rP1      ( Register for reading joy pad info [R/W] )
: [rP1] rP1 ]* ;
$FF01 constant rSB      ( Serial Transfer Data [R/W] )
: [rSB] rSB ]* ;
$FF02 constant rSC      ( Serial I/O Control [R/W] )
: [rSC] rSC ]* ;

$FF04 constant rDIV     ( Divider register [R/W] )
: [rDIV] rDIV ]* ;
$FF05 constant rTIMA    ( Timer counter [R/W] )
: [rTIMA] rTIMA ]* ;
$FF06 constant rTMA     ( Timer modulo [R/W] )
: [rTMA] rTMA ]* ;
$FF07 constant rTAC     ( Timer control [R/W] )
: [rTAC] rTAC ]* ;

$FF4D constant rKEY1    ( [CGB only] CPU speed switching [R/W] )
: [rKEY1] rKEY1 ]* ;
$FF56 constant rRP      ( [CGB only] Infrared communication port [R/W] )
: [rRP] rRP ]* ;

( Bank control registers )
$FF4F constant rVBK     ( [CGB only] VRAM bank specification [R/W] )
: [rVBK] rVBK ]* ;
$FF70 constant rSVBK    ( [CGB only] WRAM Bank specification [R/W] )
: [rSVBK] rSVBK ]* ;

( Interrupt flags )
$FF0F constant rIF      ( Interrupt Flag [R/W] )
: [rIF] rIF ]* ;
$FFFF constant rIE      ( Interrupt Enable [R/W] )
: [rIE] rIE ]* ;
\ TODO: IME?

( Sound channel 1 registers )
$FF10 constant rNR10    ( Sweep register [R/W] )
: [rNR10] rNR10 ]* ;
$FF11 constant rNR11    ( Sound length / wave pattern duty [R/W] )
: [rNR11] rNR11 ]* ;
$FF12 constant rNR12    ( Envelope [R/W] )
: [rNR12] rNR12 ]* ;
$FF13 constant rNR13    ( Frequency lo [W] )
: [rNR13] rNR13 ]* ;
$FF14 constant rNR14    ( Frequency hi [W] )
: [rNR14] rNR14 ]* ;

( Sound channel 2 registers )
$FF16 constant rNR21    ( Sound length / wave pattern duty [R/W] )
: [rNR21] rNR21 ]* ;
$FF17 constant rNR22    ( Envelope [R/W] )
: [rNR22] rNR22 ]* ;
$FF18 constant rNR23    ( Frequency lo [W] )
: [rNR23] rNR23 ]* ;
$FF19 constant rNR24    ( Frequency hi [W] )
: [rNR24] rNR24 ]* ;

( Sound channel 3 registers )
$FF1A constant rNR30    ( Sound on/off [R/W] )
: [rNR30] rNR30 ]* ;
$FF1B constant rNR31    ( Sound length [R/W] )
: [rNR31] rNR31 ]* ;
$FF1C constant rNR32    ( Select output level )
: [rNR32] rNR32 ]* ;
$FF1D constant rNR33    ( Frequency lo [W] )
: [rNR33] rNR33 ]* ;
$FF1E constant rNR34    ( Frequency hi [W] )
: [rNR34] rNR34 ]* ;

( Sound channel 4 registers )
$FF20 constant rNR41    ( Sound length [R/W] )
: [rNR41] rNR41 ]* ;
$FF21 constant rNR42    ( Envelope [R/W] )
: [rNR42] rNR42 ]* ;
$FF22 constant rNR43    ( Polynomial counter [W] )
: [rNR43] rNR43 ]* ;
$FF23 constant rNR44    ( Initialise/length [R/W] )
: [rNR44] rNR44 ]* ;

( Sound control registers )
$FF24 constant rNR50    ( Channel control / ON-OFF / Volume [R/W] )
: [rNR50] rNR50 ]* ;
$FF25 constant rNR51    ( Selection of sound output terminal [R/W] )
: [rNR51] rNR51 ]* ;
$FF26 constant rNR52    ( Sound on/off [R/W] )
: [rNR52] rNR52 ]* ;

( Alternative sound register names )
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
$FF40 constant rLCDC    ( LCD control [R/W] )
: [rLCDC] rLCDC ]* ;
$FF41 constant rSTAT    ( LCD status [R/W] )
: [rSTAT] rSTAT ]* ;
$FF42 constant rSCY     ( Scroll Y [R/W] )
: [rSCY] rSCY ]* ;
$FF43 constant rSCX     ( Scroll X [R/W] )
: [rSCX] rSCX ]* ;
$FF44 constant rLY      ( LCDC Y-Coordinate [144->153 is VBlank period] [R] )
: [rLY] rLY ]* ;
$FF45 constant rLYC     ( LY Compare [R/W] )
: [rLYC] rLYC ]* ;
$FF46 constant rDMA     ( DMA Transfer and Start Address [W] )
: [rDMA] rDMA ]* ;
$FF47 constant rGBP     ( BG Palette Data [W] )
: [rGBP] rGBP ]* ;
$FF48 constant rOBP0    ( Object Palette 0 Data [W] )
: [rOBP0] rOBP0 ]* ;
$FF49 constant rOBP1    ( Object Palette 1 Data [W] )
: [rOBP1] rOBP1 ]* ;
$FF4A constant rWY      ( Window Y Position [R/W] )
: [rWY] rWY ]* ;
$FF4B constant rWX      ( Window X Position [R/W] )
: [rWX] rWX ]* ;

$FF51 constant rHDMA1   ( [CGB only] Higher-order address of HDMAtransfer source [W] )
: [rHDMA1] rHDMA1 ]* ;
$FF52 constant rHDMA2   ( [CGB only] Lower-order address of HDMAtransfer source [W] )
: [rHDMA2] rHDMA2 ]* ;
$FF53 constant rHDMA3   ( [CGB only] Higher-order address of HDMAtransfer destination [W] )
: [rHDMA3] rHDMA3 ]* ;
$FF54 constant rHDMA4   ( [CGB only] Lower-order address of HDMAtransfer destination [W] )
: [rHDMA4] rHDMA4 ]* ;
$FF55 constant rHDMA5   ( [CGB only] H-blank and general-purpose HDMA control [W] )
: [rHDMA5] rHDMA5 ]* ;

$FF68 constant rBCPS    ( [CGB only] Color palette BG write specification [R/W] )
: [rBCPS] rBCPS ]* ;
$FF69 constant rGCPD    ( [CGB only] Color palette BG write data [R/W] )
: [rGCPD] rGCPD ]* ;
$FF6A constant rOCPS    ( [CGB only] Color palette OBJ write specification [R/W] )
: [rOCPS] rOCPS ]* ;
$FF6B constant rOCPD    ( [CGB only] Color palette OBJ write data [R/W] )
: [rOCPD] rOCPD ]* ;

[endhost]

$FF00 constant _HW

$8000 constant _VRAM         ( $8000->$A000 )
$9800 constant _SCRN0        ( $9800->$9BFF )
$9C00 constant _SCRN1        ( $9C00->$9FFF )
$A000 constant _CARTRAM      ( $A000->$BFFF )
$C000 constant _RAM          ( $C000->$E000 )
$F800 constant _HRAM         ( $F800->$FFFE )
$FE00 constant _OAMRAM       ( $FE00->$FE9F )
$FF30 constant _AUD3WAVERAM  ( $FF30->$FF3F )

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

[endasm]
