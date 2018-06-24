require ./cpu.fs
require ./gbhw.fs
require ./memory.fs
require ./input.fs
require ./bits.fs

create TileData
include ./ibm-font.fs

variable cursor-x
variable cursor-y

: cursor-addr ( -- addr )
  _SCRN0 cursor-x @ +
  SCRN_VY_B cursor-y @ *
  + ;

: copy-font
  TileData _VRAM [ 256 8 * ]L cmovemono ;

: at-xy ( x y -- )
  cursor-y !
  cursor-x ! ;

\ Clear the screen
: page
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L bl fill
  0 0 at-xy ;

: type ( addr u -- )
  cursor-addr swap cmovevideo ;

:m ."
  postpone s"
  postpone type
; immediate


\ Wait until a key is pressed and return
: key
  key-state
  dup
  begin
    halt
    key-state tuck <>
  until
  ( old new )
  tuck xor and
  \ At this point the TOS contains the bits with the new keys that
  \ were pressed. However, key should always return 1 key, not many!
  \ We extract the key with the highest priority (the rightmost 1-bit
  \ set) to disambiguate.
  irm1b ;

: reset-palette
  %11100100 rGBP c! ;

: reset-window-scroll
  0 rSCX c!
  0 rSCY c! ;

: init-term
  disable-interrupts
  reset-palette
  reset-window-scroll
  init-input
  disable-lcd
  copy-font
  enable-lcd
  enable-interrupts
;
