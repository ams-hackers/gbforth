title: SPRITE-DEMO
makercode: TK

require sprites.fs
require ibm-font.fs
require input.fs
require lcd.fs

( program start )

#8     constant spr-height
#8     constant spr-width
#8     constant x-start
#16    constant y-start
SCRN_X constant x-end
SCRN_Y constant y-end
x-end 2/ x-start + spr-width  2/ - constant x-middle
y-end 2/ y-start + spr-height 2/ - constant y-middle

0 SPRITE player
1 SPRITE north
2 SPRITE northeast
3 SPRITE east
4 SPRITE southeast
5 SPRITE south
6 SPRITE southwest
7 SPRITE west
8 SPRITE northwest

: playerX+!   player spr-x    lcd-wait-vblank +! ;
: playerY+!   player spr-y    lcd-wait-vblank +! ;
: playerTile! player spr-tile lcd-wait-vblank c! ;

: handle-input
  begin
    key-state
    dup k-right and if  1 playerX+!   then
    dup k-left  and if -1 playerX+!   then
    dup k-up    and if -1 playerY+!   then
    dup k-down  and if  1 playerY+!   then
    dup k-a     and if  1 playerTile! then
    dup k-b     and if  2 playerTile! then
    \ If there no key pressed, wait for one
    0= if halt then
  again ;

: clear-background
  lcd-wait-vblank
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L blank ;

: init-player-sprite
  lcd-wait-vblank
  #1        player spr-tile c!
  %00000000 player spr-opts c!
  x-middle  player spr-x    c!
  y-middle  player spr-y    c! ;

: init-corner-sprites
  lcd-wait-vblank
  #31       north spr-tile c!
  %00000000 north spr-opts c!
  x-middle  north spr-x    c!
  y-start   north spr-y    c!

  lcd-wait-vblank
  #4        northeast spr-tile c!
  %00000000 northeast spr-opts c!
  x-end     northeast spr-x    c!
  y-start   northeast spr-y    c!

  lcd-wait-vblank
  #17       east spr-tile c!
  %00000000 east spr-opts c!
  x-end     east spr-x    c!
  y-middle  east spr-y    c!

  lcd-wait-vblank
  #6        southeast spr-tile c!
  %00000000 southeast spr-opts c!
  x-end     southeast spr-x    c!
  y-end     southeast spr-y    c!

  lcd-wait-vblank
  #30       south spr-tile c!
  %00000000 south spr-opts c!
  x-middle  south spr-x    c!
  y-end     south spr-y    c!

  lcd-wait-vblank
  #5        southwest spr-tile c!
  %00000000 southwest spr-opts c!
  x-start   southwest spr-x    c!
  y-end     southwest spr-y    c!

  lcd-wait-vblank
  #16       west spr-tile c!
  %00000000 west spr-opts c!
  x-start   west spr-x    c!
  y-middle  west spr-y    c!

  lcd-wait-vblank
  #3        northwest spr-tile c!
  %00000000 northwest spr-opts c!
  x-start   northwest spr-x    c!
  y-start   northwest spr-y    c! ;

: main
  init-sprites
  install-font
  init-input

  clear-background
  init-corner-sprites
  init-player-sprite

  handle-input ;
