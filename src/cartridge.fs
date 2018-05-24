require ./asm.fs
require ./user.fs

also gb-assembler

( Constants used in cartridge header )

( Game Boy Color flag [$0143] )
$00 constant CGB_INCOMPATIBLE ( Does not use CGB functions, but still works on CGB )
$80 constant CGB_COMPATIBLE ( Uses CGB functions, but also works on DMG )
$C0 constant CGB_EXCLUSIVE ( Uses CGB functions, DMG not supported )

( Super Game Boy flag [$0146] )
$00 constant SGB_DISABLED ( Does not use SGB functions, but still works on SGB )
$03 constant SGB_ENABLED  ( Uses SGB functions. Requires $014B ==> $33, )

( Cartridge type [$0147] )
\ specifies whether the cart contains
\ ROM, a Memory Bank Controller (MBC), SRAM, Backup Battery (BAT),
\ and extra hardware like Timer (RTC) or Rumble.
$00 constant CART_TYPE_ROM
$01 constant CART_TYPE_ROM_MBC1
$02 constant CART_TYPE_ROM_MBC1_RAM
$03 constant CART_TYPE_ROM_MBC1_RAM_BAT
$05 constant CART_TYPE_ROM_MBC2
$06 constant CART_TYPE_ROM_MBC2_BAT     \ Not sure which is correct
$06 constant CART_TYPE_ROM_MBC2_RAM_BAT \ Ram/non-ram?
$08 constant CART_TYPE_ROM_RAM
$09 constant CART_TYPE_ROM_RAM_BAT
$0F constant CART_TYPE_ROM_MBC3_RTC_BAT
$10 constant CART_TYPE_ROM_MBC3_RTC_RAM_BAT
$0B constant CART_TYPE_ROM_MMM01
$0C constant CART_TYPE_ROM_MMM01_RAM
$0D constant CART_TYPE_ROM_MMM01_RAM_BAT
$11 constant CART_TYPE_ROM_MBC3
$12 constant CART_TYPE_ROM_MBC3_RAM
$13 constant CART_TYPE_ROM_MBC3_RAM_BAT
$19 constant CART_TYPE_ROM_MBC5
$1A constant CART_TYPE_ROM_MBC5_RAM
$1B constant CART_TYPE_ROM_MBC5_RAM_BAT
$1C constant CART_TYPE_ROM_MBC5_RUMBLE
$1D constant CART_TYPE_ROM_MBC5_RUMBLE_RAM
$1E constant CART_TYPE_ROM_MBC5_RUMBLE_RAM_BAT
$20 constant CART_TYPE_ROM_MBC6_RAM_BAT
$22 constant CART_TYPE_ROM_MBC7_RAM_BAT_ACCELEROMETER
$FC constant CART_TYPE_ROM_POCKET_CAMERA
$FD constant CART_TYPE_ROM_BANDAI_TAMA5
$FE constant CART_TYPE_ROM_HUC3
$FF constant CART_TYPE_ROM_HUC1_RAM_BAT

( ROM size [$0148] )
$00 constant ROM_SIZE_32KBYTE
$00 constant ROM_SIZE_256KBIT
$01 constant ROM_SIZE_64KBYTE
$01 constant ROM_SIZE_512KBIT
$02 constant ROM_SIZE_128KBYTE
$02 constant ROM_SIZE_1MBIT
$03 constant ROM_SIZE_256KBYTE
$03 constant ROM_SIZE_2MBIT
$04 constant ROM_SIZE_512KBYTE
$04 constant ROM_SIZE_4MBIT
$05 constant ROM_SIZE_1MBYTE
$05 constant ROM_SIZE_8MBIT
$06 constant ROM_SIZE_2MBYTE
$06 constant ROM_SIZE_16MBIT
$07 constant ROM_SIZE_4MBYTE
$07 constant ROM_SIZE_32MBIT
$08 constant ROM_SIZE_8MBYTE
$08 constant ROM_SIZE_64MBIT

( RAM size [$0149] )
$00 constant RAM_SIZE_NONE
$00 constant RAM_SIZE_0KBYTE
$00 constant RAM_SIZE_0KBIT
$01 constant RAM_SIZE_2KBYTE
$01 constant RAM_SIZE_16KBIT
$02 constant RAM_SIZE_8KBYTE
$02 constant RAM_SIZE_64KBIT
$03 constant RAM_SIZE_32KBYTE
$03 constant RAM_SIZE_256KBIT
$04 constant RAM_SIZE_128KBYTE
$04 constant RAM_SIZE_1MBIT
$05 constant RAM_SIZE_64KBYTE
$03 constant RAM_SIZE_512KBIT

( Destination code [$014A] )
$00 constant DEST_JAPAN
$01 constant DEST_OTHER

( Old licensee code [$014B] )
\ Nowadays unused in favour of makercode at $0144
$33 constant USE_MAKER_CODE

( Words to modify header values )

( Boot logo [$0104-0133] )
: nintendo-logo,
  $ce rom, $ed rom, $66 rom, $66 rom, $cc rom, $0d rom, $00 rom, $0b rom,
  $03 rom, $73 rom, $00 rom, $83 rom, $00 rom, $0c rom, $00 rom, $0d rom,
  $00 rom, $08 rom, $11 rom, $1f rom, $88 rom, $89 rom, $00 rom, $0e rom,
  $dc rom, $cc rom, $6e rom, $e6 rom, $dd rom, $dd rom, $d9 rom, $99 rom,
  $bb rom, $bb rom, $67 rom, $63 rom, $6e rom, $0e rom, $ec rom, $cc rom,
  $dd rom, $dc rom, $99 rom, $9f rom, $bb rom, $b9 rom, $33 rom, $3e rom, ;

: parse-line ( -- addr u )
  #10 parse ;

: title:
  parse-line
  dup #15 > abort" Title is too long"
  $0134 offset>addr swap move ;

: gamecode:
  parse-line
  dup #4 > abort" Game Code is too long"
  $013F offset>addr swap move ;

: makercode:
  parse-line
  dup #2 > abort" Maker Code is too long"
  $0144 offset>addr swap move
  USE_MAKER_CODE $014B rom! ;

: header-complement
  0
  $014D $0134 ?do
    i rom@ +
  loop
  $19 + negate ;

: fix-header-complement
  header-complement $014D rom! ;

: global-checksum
  0
  $014E $0 ?do
    i rom@ +
  loop
  rom-size $0150 ?do
    i rom@ +
  loop
  $FFFF and ;

: fix-global-checksum
  global-checksum dup
  higher-byte $014E rom!
  lower-byte $014F rom! ;

( Cartridge structure )

$00 ==> ( restart $00 address )
$08 ==> ( restart $08 address )
$10 ==> ( restart $10 address )
$18 ==> ( restart $18 address )
$20 ==> ( restart $20 address )
$28 ==> ( restart $28 address )
$30 ==> ( restart $30 address )
$38 ==> ( restart $38 address )
$40 ==> ( vertical blank interrupt start address )
$48 ==> ( LCDC status interrupt start address )
$50 ==> ( timer overflowInterrupt start address )
$58 ==> ( serial transfer completion interrupt start address )
$60 ==> ( high-to-low of p10 interrupt start address )
$68 ==> ( high-to-low of p11 interrupt start address )
$70 ==> ( high-to-low of p12 interrupt start address )
$78 ==> ( high-to-low of p13 interrupt start address )

$100 ==> ( start entry point [$0100-$0103] )

presume main

[user-definitions]
' main alias main
' title: alias title:
[end-user-definitions]

nop,
main jp,

( start header [$0100-$014F] )

$0104 ==> nintendo-logo,            ( boot logo )
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

( header end [$0150...] )

previous
