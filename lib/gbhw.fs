[asm]

[host]

( port/mode registers )
: [rP1]    $FF00 ]* ;  ( Register for reading joy pad info [R/W] )
: [rSB]    $FF01 ]* ;  ( Serial Transfer Data [R/W] )
: [rSC]    $FF02 ]* ;  ( Serial I/O Control [R/W] )

: [rDIV]   $FF04 ]* ;  ( Divider register [R/W] )
: [rTIMA]  $FF05 ]* ;  ( Timer counter [R/W] )
: [rTMA]   $FF06 ]* ;  ( Timer modulo [R/W] )
: [rTAC]   $FF07 ]* ;  ( Timer control [R/W] )

: [rKEY1]  $FF4D ]* ;  ( [CGB only] CPU speed switching [R/W] )
: [rRP]    $FF56 ]* ;  ( [CGB only] Infrared communication port [R/W] )

( Bank control registers )
: [rVBK]   $FF4F ]* ;  ( [CGB only] VRAM bank specification [R/W] )
: [rSVBK]  $FF70 ]* ;  ( [CGB only] WRAM Bank specification [R/W] )

( Interrupt flags )
: [rIF]    $FF0F ]* ;  ( Interrupt Flag [R/W] )
: [rIE]    $FFFF ]* ;  ( Interrupt Enable [R/W] )
\ TODO: IME?

( Sound channel 1 registers )
: [rNR10]  $FF10 ]* ;  ( Sweep register [R/W] )
: [rNR11]  $FF11 ]* ;  ( Sound length / wave pattern duty [R/W] )
: [rNR12]  $FF12 ]* ;  ( Envelope [R/W] )
: [rNR13]  $FF13 ]* ;  ( Frequency lo [W] )
: [rNR14]  $FF14 ]* ;  ( Frequency hi [W] )

( Sound channel 2 registers )
: [rNR21]  $FF16 ]* ;  ( Sound length / wave pattern duty [R/W] )
: [rNR22]  $FF17 ]* ;  ( Envelope [R/W] )
: [rNR23]  $FF18 ]* ;  ( Frequency lo [W] )
: [rNR24]  $FF19 ]* ;  ( Frequency hi [W] )

( Sound channel 3 registers )
: [rNR30]  $FF1A ]* ;  ( Sound on/off [R/W] )
: [rNR31]  $FF1B ]* ;  ( Sound length [R/W] )
: [rNR32]  $FF1C ]* ;  ( Select output level )
: [rNR33]  $FF1D ]* ;  ( Frequency lo [W] )
: [rNR34]  $FF1E ]* ;  ( Frequency hi [W] )

( Sound channel 4 registers )
: [rNR41]  $FF20 ]* ;  ( Sound length [R/W] )
: [rNR42]  $FF21 ]* ;  ( Envelope [R/W] )
: [rNR43]  $FF22 ]* ;  ( Polynomial counter [W] )
: [rNR44]  $FF23 ]* ;  ( Initialise/length [R/W] )

( Sound control registers )
: [rNR50]  $FF24 ]* ;  ( Channel control / ON-OFF / Volume [R/W] )
: [rNR51]  $FF25 ]* ;  ( Selection of sound output terminal [R/W] )
: [rNR52]  $FF26 ]* ;  ( Sound on/off [R/W] )

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
: [rLCDC]  $FF40 ]* ;  ( LCD control [R/W] )
: [rSTAT]  $FF41 ]* ;  ( LCD status [R/W] )
: [rSCY]   $FF42 ]* ;  ( Scroll Y [R/W] )
: [rSCX]   $FF43 ]* ;  ( Scroll X [R/W] )
: [rLY]    $FF44 ]* ;  ( LCDC Y-Coordinate [144->153 is VBlank period] [R] )
: [rLYC]   $FF45 ]* ;  ( LY Compare [R/W] )
: [rDMA]   $FF46 ]* ;  ( DMA Transfer and Start Address [W] )
: [rGBP]   $FF47 ]* ;  ( BG Palette Data [W] )
: [rOBP0]  $FF48 ]* ;  ( Object Palette 0 Data [W] )
: [rOBP1]  $FF49 ]* ;  ( Object Palette 1 Data [W] )
: [rWY]    $FF4A ]* ;  ( Window Y Position [R/W] )
: [rWX]    $FF4B ]* ;  ( Window X Position [R/W] )

: [rHDMA1] $FF51 ]* ;  ( [CGB only] Higher-order address of HDMAtransfer source [W] )
: [rHDMA2] $FF52 ]* ;  ( [CGB only] Lower-order address of HDMAtransfer source [W] )
: [rHDMA3] $FF53 ]* ;  ( [CGB only] Higher-order address of HDMAtransfer destination [W] )
: [rHDMA4] $FF54 ]* ;  ( [CGB only] Lower-order address of HDMAtransfer destination [W] )
: [rHDMA5] $FF55 ]* ;  ( [CGB only] H-blank and general-purpose HDMA control [W] )

: [rBCPS]  $FF68 ]* ;  ( [CGB only] Color palette BG write specification [R/W] )
: [rGCPD]  $FF69 ]* ;  ( [CGB only] Color palette BG write data [R/W] )
: [rOCPS]  $FF6A ]* ;  ( [CGB only] Color palette OBJ write specification [R/W] )
: [rOCPD]  $FF6B ]* ;  ( [CGB only] Color palette OBJ write data [R/W] )

[endhost]

$FF00 constant _HW

$8000 constant _VRAM         ( $8000->$A000 )
$9800 constant _SCRN0        ( $9800->$9BFF )
$9C00 constant _SCRN1        ( $9C00->$9FFF )
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
