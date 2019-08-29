require ./gbhw.fs
require ./memory.fs
require ./lcd.fs
require ./cpu.fs

[host]

\ Define a line for a 1-bit (2 colours) tile:
\ Palette 0: `.`
\ Palette 3: `X`
: l:
  parse-name
  dup 8 <> if true abort" The tile line length should be 8 pixels!" then
  0 swap
  0 ?do
    1 lshift
    over I + c@ case
      [char] X of 1 or endof
      [char] . of      endof
      true abort" Wrong character! Expected a 'X' or '.'"
    endcase
  loop
  nip
  [target] c, [host] ;

[target]

\ copy a monochromatic tileset
: install-tileset ( c-addr n -- )
  disable-interrupts
  disable-lcd
  8 * _VRAM swap cmovemono
  enable-lcd
  enable-interrupts ;
