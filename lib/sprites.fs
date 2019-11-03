require ./cpu.fs
require ./gbhw.fs
require ./lcd.fs
require ./struct.fs

MEM> ROM

struct
  1 chars field spr-y
  1 chars field spr-x
  1 chars field spr-tile
  1 chars field spr-opts
end-struct sprite%

>MEM

:m SPRITE sprite% * _OAM + CONSTANT ;

: erase-oam
  disable-interrupts
  disable-lcd
  _OAM [ 40 4 * ]L erase
  enable-lcd
  enable-interrupts ;

: reset-obj-palette
  %11100100 rOBP0 c! ;

: enable-sprites
  rLCDC c@
  [ LCDCF_OBJ8
    LCDCF_OBJON or ]L or
  rLCDC c! ;

: init-sprites
  erase-oam
  reset-obj-palette
  enable-sprites ;
