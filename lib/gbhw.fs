also gb-assembler

( Special registers! )
[host]
: [rGBP]  $FF47 ]* ;
: [rSCY]  $FF42 ]* ;
: [rSCX]  $FF43 ]* ;

: [rLCDC] $FF40 ]* ;

: [rLY]   $FF44 ]* ;
: [rSTAT] $FF41 ]* ;
[endhost]

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

$FF00 constant _HW

$8000 constant _VRAM         ( $8000->$A000 )
$9800 constant _SCRN0        ( $9800->$9BFF)
$9C00 constant _SCRN1        ( $9C00->$9FFF )
$C000 constant _RAM          ( $C000->$E000 )
$F800 constant _HRAM         ( $F800->$FFFE )
$FE00 constant _OAMRAM       ( $FE00->$FE9F )
$FF30 constant _AUD3WAVERAM  ( $FF30->$FF3F )

#160 constant SCRN_X    ( Width of screen in pixels )
#144 constant SCRN_Y    ( Height of screen in pixels )
#20  constant SCRN_X_B  ( Width of screen in bytes )
#18  constant SCRN_Y_B  ( Height of screen in bytes )

#256 constant SCRN_VX   ( Virtual width of screen in pixels )
#256 constant SCRN_VY   ( Virtual height of screen in pixels )
#32  constant SCRN_VX_B ( Virtual width of screen in bytes )
#32  constant SCRN_VY_B ( Virtual height of screen in bytes )


%01000000 constant STATF_LYC    ( LYCEQULY Coincidence [Selectable])
%00100000 constant STATF_MODE10 ( Mode 10)
%00010000 constant STATF_MODE01 ( Mode 01 [V-Blank])
%00001000 constant STATF_MODE00 ( Mode 00 [H-Blank])
%00000100 constant STATF_LYCF   ( Coincidence Flag)
%00000000 constant STATF_HB     ( H-Blank)
%00000001 constant STATF_VB     ( V-Blank)
%00000010 constant STATF_OAM    ( OAM-RAM is used by system)
%00000011 constant STATF_LCD    ( Both OAM and VRAM used by system)
%00000010 constant STATF_BUSY   ( When set, VRAM access is unsafe )


previous
