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
$06 constant CART_TYPE_ROM_MBC2_BAT
$08 constant CART_TYPE_ROM_RAM
$09 constant CART_TYPE_ROM_RAM_BAT
$0B constant CART_TYPE_ROM_MMM01
$0C constant CART_TYPE_ROM_MMM01_RAM
$0D constant CART_TYPE_ROM_MMM01_RAM_BAT
$0F constant CART_TYPE_ROM_MBC3_RTC_BAT
$10 constant CART_TYPE_ROM_MBC3_RTC_RAM_BAT
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
$05 constant RAM_SIZE_512KBIT

( Destination code [$014A] )
$00 constant DEST_JAPAN
$01 constant DEST_OTHER

( Old licensee code [$014B] )
\ Nowadays unused in favour of makercode at $0144
$33 constant USE_MAKER_CODE

( Boot logo [$0104-0133] )
:m boot-logo,
  $ce c, $ed c, $66 c, $66 c, $cc c, $0d c, $00 c, $0b c,
  $03 c, $73 c, $00 c, $83 c, $00 c, $0c c, $00 c, $0d c,
  $00 c, $08 c, $11 c, $1f c, $88 c, $89 c, $00 c, $0e c,
  $dc c, $cc c, $6e c, $e6 c, $dd c, $dd c, $d9 c, $99 c,
  $bb c, $bb c, $67 c, $63 c, $6e c, $0e c, $ec c, $cc c,
  $dd c, $dc c, $99 c, $9f c, $bb c, $b9 c, $33 c, $3e c, ;
