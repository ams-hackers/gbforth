also gb-assembler

( Special registers! )
[host]
: [rP1]    $FF00 ]* ;  ( Register for reading joy pad info [R/W] )
: [rSB]    $FF01 ]* ;  ( Serial Transfer Data [R/W] )
: [rSC]    $FF02 ]* ;  ( Serial I/O Control [R/W] )

: [rDIV]   $FF04 ]* ;  ( Divider register [R/W] )
: [rTIMA]  $FF05 ]* ;  ( Timer counter [R/W] )
: [rTMA]   $FF06 ]* ;  ( Timer modulo [R/W] )
: [rTAC]   $FF07 ]* ;  ( Timer control [R/W] )

: [rIF]    $FF0F ]* ;  ( Interrupt Flag [R/W] )

\ TODO: Sound registers $FF10 - $FF3F

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

: [rKEY1]  $FF4D ]* ;  ( CPU speed switching [R/W] )

: [rVBK]   $FF4F ]* ;  ( VRAM bank specification [R/W] )

: [rHDMA1] $FF51 ]* ;  ( Higher-order address of HDMAtransfer source [W] )
: [rHDMA2] $FF52 ]* ;  ( Lower-order address of HDMAtransfer source [W] )
: [rHDMA3] $FF53 ]* ;  ( Higher-order address of HDMAtransfer destination [W] )
: [rHDMA4] $FF54 ]* ;  ( Lower-order address of HDMAtransfer destination [W] )
: [rHDMA5] $FF55 ]* ;  ( H-blank and general-purpose HDMA control [W] )
: [rRP]    $FF56 ]* ;  ( Infrared communication port [R/W] )

: [rBCPS]  $FF68 ]* ;  ( Color palette BG write specification [R/W] )
: [rGCPD]  $FF69 ]* ;  ( Color palette BG write data [R/W] )
: [rOCPS]  $FF6A ]* ;  ( Color palette OBJ write specification [R/W] )
: [rOCPD]  $FF6B ]* ;  ( Color palette OBJ write data [R/W] )

: [rSVBK]  $FF70 ]* ;  ( WRAM Bank specification [R/W] )

\ TODO: Registers $FE00 - $FE03

: [rIE]    $FFFF ]* ;  ( Interrupt Enable [R/W] )
[endhost]

$FF00 constant _HW

$8000 constant _VRAM         ( $8000->$A000 )
$9800 constant _SCRN0        ( $9800->$9BFF)
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
  Sound control registers
  *************************
)

\ Channel control / ON-OFF / Volume [R/W]
\ Bit 7   - Vin->SO2 ON/OFF (Vin??)
\ Bit 6-4 - SO2 output level (volume) (# 0-7)
\ Bit 3   - Vin->SO1 ON/OFF (Vin??)
\ Bit 2-0 - SO1 output level (volume) (# 0-7)
[host]
: [rNR50]   $FF24 ]* ;
: [rAUDVOL] [rNR50] ;
[endhost]

\ Selection of Sound output terminal [R/W]
\ Bit 7   - Output sound 4 to SO2 terminal
\ Bit 6   - Output sound 3 to SO2 terminal
\ Bit 5   - Output sound 2 to SO2 terminal
\ Bit 4   - Output sound 1 to SO2 terminal
\ Bit 3   - Output sound 4 to SO1 terminal
\ Bit 2   - Output sound 3 to SO1 terminal
\ Bit 1   - Output sound 2 to SO1 terminal
\ Bit 0   - Output sound 0 to SO1 terminal
[host]
: [rNR51] $FF25 ]* ;
: [rAUDTERM] [rNR51] ;
[endhost]

\ Sound on/off [R/W]
\ Bit 7   - All sound on/off (sets all audio regs to 0!)
\ Bit 3   - Sound 4 ON flag (doesn't work!)
\ Bit 2   - Sound 3 ON flag (doesn't work!)
\ Bit 1   - Sound 2 ON flag (doesn't work!)
\ Bit 0   - Sound 1 ON flag (doesn't work!)
[host]
: [rNR52] $FF26 ]* ;
: [rAUDENA] [rNR52] ;
[endhost]

(
  *************************
  SoundChannel #1 registers
  *************************
)

\ Sweep register [R/W]
\ Bit 6-4 - Sweep Time
\ Bit 3   - Sweep Increase/Decrease
\           0: Addition    (frequency increases???)
\           1: Subtraction (frequency increases???)
\ Bit 2-0 - Number of sweep shift (# 0-7)
\ Sweep Time: (n*7.8ms)
[host]
: [rNR10] $FF10 ]* ;
: [rAUD1SWEEP] [rNR10] ;
[endhost]

\ Sound length/Wave pattern duty [R/W]
\ Bit 7-6 - Wave Pattern Duty (00:12.5% 01:25% 10:50% 11:75%)
\ Bit 5-0 - Sound length data (# 0-63)
[host]
: [rNR11] $FF11 ]* ;
: [rAUD1LEN] [rNR11] ;
[endhost]

\ Envelope [R/W]
\ Bit 7-4 - Initial value of envelope
\ Bit 3   - Envelope UP/DOWN
\           0: Decrease
\           1: Range of increase
\ Bit 2-0 - Number of envelope sweep (# 0-7)
[host]
: [rNR12] $FF12 ]* ;
: [rAUD1ENV] [rNR12] ;
[endhost]

\ Frequency lo [W]
[host]
: [rNR13] $FF13 ]* ;
: [rAUD1LOW] [rNR13] ;
[endhost]

\ Frequency hi [W]
\ Bit 7   - Initial (when set, sound restarts)
\ Bit 6   - Counter/consecutive selection
\ Bit 2-0 - Frequency's higher 3 bits
[host]
: [rNR14] $FF14 ]* ;
: [rAUD1HIGH] [rNR14] ;
[endhost]

(
  *************************
  SoundChannel #2 registers
  *************************
)

\ Sound Length; Wave Pattern Duty [R/W]
\ see AUD1LEN for info
[host]
: [rNR21] $FF16 ]* ;
: [rAUD2LEN] [rNR21] ;
[endhost]

\ Envelope [R/W]
\ see AUD1ENV for info
[host]
: [rNR22] $FF17 ]* ;
: [rAUD2ENV] [rNR22] ;
[endhost]

\ Frequency lo [W]
[host]
: [rNR23] $FF18 ]* ;
: [rAUD2LOW] [rNR23] ;
[endhost]

\ Frequency hi [W]
\ see AUD1HIGH for info
[host]
: [rNR24] $FF19 ]* ;
: [rAUD2HIGH] [rNR24] ;
[endhost]

(
  *************************
  SoundChannel #3 registers
  *************************
)

\ Sound on/off [R/W]
\ Bit 7   - Sound ON/OFF (1EQUON,0EQUOFF)
[host]
: [rNR30] $FF1A ]* ;
: [rAUD3ENA] [rNR30] ;
[endhost]

\ Sound length [R/W]
\ Bit 7-0 - Sound length
[host]
: [rNR31] $FF1B ]* ;
: [rAUD3LEN] [rNR31] ;
[endhost]

\ Select output level
\ Bit 6-5 - Select output level
\           00: 0/1 (mute)
\           01: 1/1
\           10: 1/2
\           11: 1/4
[host]
: [rNR32] $FF1C ]* ;
: [rAUD3LEVEL] [rNR32] ;
[endhost]

\ Frequency lo [W]
\ see AUD1LOW for info
[host]
: [rNR33] $FF1D ]* ;
: [rAUD3LOW] [rNR33] ;
[endhost]

\ Frequency hi [W]
\ see AUD1HIGH for info
[host]
: [rNR34] $FF1E ]* ;
: [rAUD3HIGH] [rNR34] ;
[endhost]

\ Sound length [R/W]
\ Bit 5-0 - Sound length data (# 0-63)
[host]
: [rNR41] $FF20 ]* ;
: [rAUD4LEN] [rNR41] ;
[endhost]

\ Envelope [R/W]
\ see AUD1ENV for info
[host]
: [rNR42] $FF21 ]* ;
: [rAUD4ENV] [rNR42] ;
[endhost]

\ Polynomial counter [R/W]
\ Bit 7-4 - Selection of the shift clock frequency of the (scf)
\           polynomial counter (0000-1101)
\           freqEQUdrf*1/2^scf (not sure)
\ Bit 3 -   Selection of the polynomial counter's step
\           0: 15 steps
\           1: 7 steps
\ Bit 2-0 - Selection of the dividing ratio of frequencies (drf)
\           000: f/4   001: f/8   010: f/16  011: f/24
\           100: f/32  101: f/40  110: f/48  111: f/56  (fEQU4.194304 Mhz)
[host]
: [rNR42_2] $FF22 ]* ;
: [rAUD4POLY] [rNR42_2] ;
[endhost]

\ (has wrong name and value (ff30) in Dr.Pan's doc!)
\ Bit 7 -   Inital
\ Bit 6 -   Counter/consecutive selection
[host]
: [rNR43] $FF23 ]* ;
: [rAUD4GO] [rNR43] ; \ silly name!
[endhost]

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

previous
