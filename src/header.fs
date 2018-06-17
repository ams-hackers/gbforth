( Cartridge structure )

require ./rom.fs
require ./asm.fs
require ./cartridge.fs

[asm]

( Boot logo [$0104-0133] )
: boot-logo,
  $ce romc, $ed romc, $66 romc, $66 romc, $cc romc, $0d romc, $00 romc, $0b romc,
  $03 romc, $73 romc, $00 romc, $83 romc, $00 romc, $0c romc, $00 romc, $0d romc,
  $00 romc, $08 romc, $11 romc, $1f romc, $88 romc, $89 romc, $00 romc, $0e romc,
  $dc romc, $cc romc, $6e romc, $e6 romc, $dd romc, $dd romc, $d9 romc, $99 romc,
  $bb romc, $bb romc, $67 romc, $63 romc, $6e romc, $0e romc, $ec romc, $cc romc,
  $dd romc, $dc romc, $99 romc, $9f romc, $bb romc, $b9 romc, $33 romc, $3e romc, ;

$0000 ==>       ( restart $0000 address )
$0008 ==>       ( restart $0008 address )
$0010 ==>       ( restart $0010 address )
$0018 ==>       ( restart $0018 address )
$0020 ==>       ( restart $0020 address )
$0028 ==>       ( restart $0028 address )
$0030 ==>       ( restart $0030 address )
$0038 ==>       ( restart $0038 address )

$0040 ==> reti, ( vertical blank interrupt start address )
$0048 ==> reti, ( LCDC status interrupt start address )
$0050 ==> reti, ( timer overflowInterrupt start address )
$0058 ==> reti, ( serial transfer completion interrupt start address )
$0060 ==> reti, ( high-to-low of p10 interrupt start address )
$0068 ==> reti, ( high-to-low of p11 interrupt start address )
$0070 ==> reti, ( high-to-low of p12 interrupt start address )
$0078 ==> reti, ( high-to-low of p13 interrupt start address )

$0100 ==> ( start entry point [$0100-$0103] )

nop,
there> jp,
named-ref> main:

( start header [$0104-$014F] )

$0104 ==> boot-logo,                ( boot logo )
$0134 ==>                           ( title )
$013F ==>                           ( manufacturer code )
$0143 ==> CGB_INCOMPATIBLE romc,    ( color GB function support )
$0144 ==>                           ( maker code )
$0146 ==> SGB_DISABLED romc,        ( gb 00 or super gameboy 03 )
$0147 ==> CART_TYPE_ROM romc,       ( cartridge type - rom only )
$0148 ==> ROM_SIZE_32KBYTE romc,    ( rom size )
$0149 ==> RAM_SIZE_NONE romc,       ( ram size )
$014A ==> DEST_OTHER romc,          ( market code jp/int )
$014B ==> USE_MAKER_CODE romc,      ( old licensee code )
$014C ==> $00 romc,                 ( mask rom version number )
$014D ==>                           ( complement checksum )
$014E ==>                           ( global checksum )

( start main code [$0150...] )
$0150 ==> main:

[endasm]
