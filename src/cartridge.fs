require ./asm.fs
require ./user.fs

also gb-assembler

( Constants used in cartridge header )

: logo
  $ce rom, $ed rom, $66 rom, $66 rom, $cc rom, $0d rom, $00 rom, $0b rom,
  $03 rom, $73 rom, $00 rom, $83 rom, $00 rom, $0c rom, $00 rom, $0d rom,
  $00 rom, $08 rom, $11 rom, $1f rom, $88 rom, $89 rom, $00 rom, $0e rom,
  $dc rom, $cc rom, $6e rom, $e6 rom, $dd rom, $dd rom, $d9 rom, $99 rom,
  $bb rom, $bb rom, $67 rom, $63 rom, $6e rom, $0e rom, $ec rom, $cc rom,
  $dd rom, $dc rom, $99 rom, $9f rom, $bb rom, $b9 rom, $33 rom, $3e rom, ;

( CGB flag [$0143] )
$80 constant CGB_SUPPORTED
$C0 constant CGB_ONLY

( SGB flag [$0146] )
$00 constant SGB_DISABLED
$03 constant SGB_ENABLED

( Cartridge type [$0147] )
$00 constant ROM_NOMBC
$01 constant ROM_MBC1
$02 constant ROM_MBC1_RAM
$03 constant ROM_MBC1_RAM_BAT
$05 constant ROM_MBC2
$06 constant ROM_MBC2_BAT
$08 constant ROM_NOMBC_RAM
$09 constant ROM_NOMBC_RAM_BAT

\ #0 constant ROM_SIZE_256KBIT
\ #1 constant ROM_SIZE_512KBIT
\ #2 constant ROM_SIZE_1M
\ #3 constant ROM_SIZE_2M
\ #4 constant ROM_SIZE_4M
\ #5 constant ROM_SIZE_8M
\ #6 constant ROM_SIZE_16M

( ROM size [$0148] )
$00 constant ROM_SIZE_32KBYTE
$01 constant ROM_SIZE_64KBYTE
$02 constant ROM_SIZE_128KBYTE
$03 constant ROM_SIZE_256KBYTE
$04 constant ROM_SIZE_512KBYTE
$05 constant ROM_SIZE_1MBYTE
$06 constant ROM_SIZE_2MBYTE
$07 constant ROM_SIZE_4MBYTE
$52 constant ROM_SIZE_1.1MBYTE
$53 constant ROM_SIZE_1.2MBYTE
$54 constant ROM_SIZE_1.5MBYTE

\ #0 constant RAM_SIZE_0KBIT
\ #1 constant RAM_SIZE_16KBIT
\ #2 constant RAM_SIZE_64KBIT
\ #3 constant RAM_SIZE_256KBIT
\ #4 constant RAM_SIZE_1MBIT

( RAM size [$0149] )
$00 constant RAM_SIZE_0KBYTE
$01 constant RAM_SIZE_2KBYTE
$02 constant RAM_SIZE_8KBYTE
$03 constant RAM_SIZE_32KBYTE
$04 constant RAM_SIZE_128KBYTE
$05 constant RAM_SIZE_64KBYTE

( Words to modify header values )

: parse-line ( -- addr u )
  #10 parse ;

: title:
  parse-line
  dup #15 > abort" Title is too long"
  $134 offset>addr swap move ;

: manufacturer:
  parse-line
  dup #4 > abort" Manufacturer Code is too long"
  $013F offset>addr swap move ;

: licensee:
  parse-line
  dup #2 > abort" Licensee Code is too long"
  $0144 offset>addr swap move
  $33 $014B rom! ;

: gbgame $00 $0143 rom! ; ( non color )


: header-complement
  0
  $014D $0134 ?do
    i rom@ +
  loop
  $19 + negate ;

: fix-header-complement
  header-complement $014D rom! ;

( Cartridge structure )

( A placeholder for values)
: $xx $42 ;

$00 ==> ( restart $00 address )
$08 ==> ( restart $08 address )
$10 ==> ( restart $10 address )
$18 ==> ( restart $18 address )
$20 ==> ( restart $20 address )
$28 ==> ( restart $28 address )
$30 ==> ( restart $30 address )
$38 ==> ( restart $38 address )
$40 ==> ( vertical blank interrupt start address )
$48 ==> ( timer overflowInterrupt start address )
$50 ==> ( LCDC status interrupt start address )
$58 ==> ( serial transfer completion interrupt start address  )
$60 ==> ( high-to-low of p10 interrupt start address )
$68 ==> ( high-to-low of p11 interrupt start address )
$70 ==> ( high-to-low of p12 interrupt start address )
$78 ==> ( high-to-low of p13 interrupt start address )

$100 ==> ( start entry point )

presume main

[user-definitions]
' main alias main
[end-user-definitions]

nop,
main jp,

( start header )

$0104 ==> ( nintendo logo )
logo
title: EXAMPLE
$143 ==> gbgame

$144 ==>
$00 rom, $00 rom, ( licensee code )
$00 rom,          ( gb 00 or super gameboy 03 )
$00 rom,          ( cartridge type - rom only )
$00 rom,          ( rom size )
$00 rom,          ( ram size )
$01 rom,          ( market code jp/int )
$33 rom,          ( licensee code )
$00 rom,          ( mask rom version number )
$xx rom,          ( complement check )
$f8 rom, $9c rom, ( checksum )

( header end )

previous
