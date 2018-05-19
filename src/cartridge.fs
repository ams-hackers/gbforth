require ./asm.fs
require ./user.fs

also gb-assembler

( Constants used in cartridge header )

#0 constant ROM_NOMBC
#1 constant ROM_MBC1
#2 constant ROM_MBC1_RAM
#3 constant ROM_MBC1_RAM_BAT
#5 constant ROM_MBC2
#6 constant ROM_MBC2_BAT
#8 constant ROM_NOMBC_RAM
#9 constant ROM_NOMBC_RAM_BAT

#0 constant ROM_SIZE_256KBIT
#1 constant ROM_SIZE_512KBIT
#2 constant ROM_SIZE_1M
#3 constant ROM_SIZE_2M
#4 constant ROM_SIZE_4M
#5 constant ROM_SIZE_8M
#6 constant ROM_SIZE_16M

#0 constant ROM_SIZE_32KBYTE
#1 constant ROM_SIZE_64KBYTE
#2 constant ROM_SIZE_128KBYTE
#3 constant ROM_SIZE_256KBYTE
#4 constant ROM_SIZE_512KBYTE
#5 constant ROM_SIZE_1MBYTE
#6 constant ROM_SIZE_2MBYTE

#0 constant RAM_SIZE_0KBIT
#1 constant RAM_SIZE_16KBIT
#2 constant RAM_SIZE_64KBIT
#3 constant RAM_SIZE_256KBIT
#4 constant RAM_SIZE_1MBIT

#0 constant RAM_SIZE_0KBYTE
#1 constant RAM_SIZE_2KBYTE
#2 constant RAM_SIZE_8KBYTE
#3 constant RAM_SIZE_32KBYTE
#4 constant RAM_SIZE_128KBYTE

( A placeholder for values)
: $xx $42 ;

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
