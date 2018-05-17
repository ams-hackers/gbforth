require ./asm.fs
require ./user.fs

also gb-assembler

( Cartridge structure )

$00 ==>         ( restart $00 address )
$08 ==>         ( restart $08 address )
$10 ==>         ( restart $10 address )
$18 ==>         ( restart $18 address )
$20 ==>         ( restart $20 address )
$28 ==>         ( restart $28 address )
$30 ==>         ( restart $30 address )
$38 ==>         ( restart $38 address )
$40 ==> reti,   ( vertical blank interrupt start address )
$48 ==> reti,   ( timer overflowInterrupt start address )
$50 ==> reti,   ( LCDC status interrupt start address )
$58 ==> reti,   ( serial transfer completion interrupt start address  )
$60 ==> reti,   ( high-to-low of p10 interrupt start address )
$68 ==>         ( high-to-low of p11 interrupt start address )
$70 ==>         ( high-to-low of p12 interrupt start address )
$78 ==>         ( high-to-low of p13 interrupt start address )

( A placeholder for values)
: $xx $42 ;

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
