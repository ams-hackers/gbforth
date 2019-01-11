require ./rom.fs
require ./asm.fs

\ The word main: gets the current ROM offset
\ and emits a JUMP to that location at the
\ entry point vector [$0100]

\ Used by the default header and possibly by
\ the user to override the default address [$0150]

[asm]
: main:
  rom-offset dup
  $0100 ==> nop, # jp,
  ==> ;
[endasm]

( Words to modify header values )

\ copied from lib/cart.fs
\ TODO: move rest of file to lib/
$33 constant USE_MAKER_CODE

: parse-line ( -- addr u )
  #10 parse ;

: title:
  parse-line
  dup #15 > abort" Title is too long (max 15 characters)"
  $0134 <rom swap move ;

: gamecode:
  parse-line
  dup #4 > abort" Game Code is too long (max 4 characters)"
  $013F <rom swap move ;

: makercode:
  parse-line
  dup #2 > abort" Maker Code is too long (max 2 characters)"
  $0144 <rom swap move
  USE_MAKER_CODE $014B romc! ;

( Words to generate the checksums )

: header-complement
  0
  $014D $0134 ?do
    i romc@ +
  loop
  $19 + negate ;

: fix-header-complement
  header-complement $014D romc! ;

: global-checksum
  0
  $014E $0 ?do
    i romc@ +
  loop
  rom-size $0150 ?do
    i romc@ +
  loop
  $FFFF and ;

: fix-global-checksum
  global-checksum dup
  higher-byte $014E romc!
  lower-byte $014F romc! ;
