( Cartridge structure )

require ./rom.fs
require ./asm.fs
require ./cartridge.fs

( Boot logo [$0104-0133] )
: boot-logo,
  $ce rom, $ed rom, $66 rom, $66 rom, $cc rom, $0d rom, $00 rom, $0b rom,
  $03 rom, $73 rom, $00 rom, $83 rom, $00 rom, $0c rom, $00 rom, $0d rom,
  $00 rom, $08 rom, $11 rom, $1f rom, $88 rom, $89 rom, $00 rom, $0e rom,
  $dc rom, $cc rom, $6e rom, $e6 rom, $dd rom, $dd rom, $d9 rom, $99 rom,
  $bb rom, $bb rom, $67 rom, $63 rom, $6e rom, $0e rom, $ec rom, $cc rom,
  $dd rom, $dc rom, $99 rom, $9f rom, $bb rom, $b9 rom, $33 rom, $3e rom, ;

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

[asm]
nop,
there> jp,
named-ref> __start:
[endasm]

( start header [$0104-$014F] )

$0104 ==> boot-logo,                ( boot logo )
$0134 ==>                           ( title )
$013F ==>                           ( manufacturer code )
$0143 ==> CGB_INCOMPATIBLE rom,     ( color GB function support )
$0144 ==>                           ( maker code )
$0146 ==> SGB_DISABLED rom,         ( gb 00 or super gameboy 03 )
$0147 ==> CART_TYPE_ROM rom,        ( cartridge type - rom only )
$0148 ==> ROM_SIZE_32KBYTE rom,     ( rom size )
$0149 ==> RAM_SIZE_NONE rom,        ( ram size )
$014A ==> DEST_OTHER rom,           ( market code jp/int )
$014B ==> USE_MAKER_CODE rom,       ( old licensee code )
$014C ==> $00 rom,                  ( mask rom version number )
$014D ==>                           ( complement checksum )
$014E ==>                           ( global checksum )

( start main code [$0150...] )
$0150 ==>
