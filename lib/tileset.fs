require ./gbhw.fs
require ./memory.fs
require ./lcd.fs
require ./cpu.fs

[host]

variable tilebyte1
variable tilebyte2

: or! ( n a-addr -- ) dup @ rot or swap ! ;
: 2*! ( a-addr -- )   dup @     2* swap ! ;

\ Define a line for a 1-bit (2 colours) tile:
\ Palette 0: `.`
\ Palette 1: `-`
\ Palette 2: `*`
\ Palette 3: `@`
: l:
  parse-name
  dup 8 <> if true abort" The tile line length should be 8 pixels!" then
  0 tilebyte1 !
  0 tilebyte2 !
  0 ?do
    tilebyte1 2*!
    tilebyte2 2*!
    dup I + c@ case
      [char] @ of 1 tilebyte1 or! 1 tilebyte2 or! endof
      [char] * of 1 tilebyte2 or!                 endof
      [char] - of 1 tilebyte1 or!                 endof
      [char] . of                                 endof
      true abort" Wrong character! Expected a '@', '*', '-' or '.'"
    endcase
  loop
  drop
  tilebyte2 @ tilebyte1 @
  [target] c, c, [host] ;

[target]

\ copy a tileset
: install-tileset ( c-addr u -- )
  disable-interrupts
  disable-lcd
  16 * _VRAM swap cmove
  enable-lcd
  enable-interrupts ;
