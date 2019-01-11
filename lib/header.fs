( Default header structure )

require ./cartridge.fs

[asm]

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

$0100 ==> ( entry point [$0100-$0103] emitted by `main:` )

( start header [$0104-$014F] )

$0104 ==> boot-logo,                ( boot logo )
$0134 ==>                           ( title )
$013F ==>                           ( manufacturer code )
$0143 ==> CGB_INCOMPATIBLE c,       ( color GB function support )
$0144 ==>                           ( maker code )
$0146 ==> SGB_DISABLED c,           ( gb 00 or super gameboy 03 )
$0147 ==> CART_TYPE_ROM c,          ( cartridge type - rom only )
$0148 ==> ROM_SIZE_32KBYTE c,       ( rom size )
$0149 ==> RAM_SIZE_NONE c,          ( ram size )
$014A ==> DEST_OTHER c,             ( market code jp/int )
$014B ==> USE_MAKER_CODE c,         ( old licensee code )
$014C ==> $00 c,                    ( mask rom version number )
$014D ==>                           ( complement checksum )
$014E ==>                           ( global checksum )

( start of main code [$0150...] )
$0150 ==> main:

[endasm]
