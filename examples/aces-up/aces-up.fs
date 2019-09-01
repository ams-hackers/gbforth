title: ACES UP
makercode: TK

require term.fs
require sprites.fs
require random.fs
require input.fs
require gbhw.fs
require lcd.fs
require stack.fs
require time.fs
require sfx.fs
require ./card-tiles.fs

4 CONSTANT #suits
13 CONSTANT #ranks
#suits #ranks * CONSTANT #cards

CREATE stock #cards allot

13 STACK pile-1
13 STACK pile-2
13 STACK pile-3
13 STACK pile-4

VALUE foundation

VARIABLE #dealt
VARIABLE score
VARIABLE hand-ix

0 SPRITE hand1
1 SPRITE hand2
2 SPRITE hand3
3 SPRITE hand4

: init-stock
  #suits 0 DO
    #ranks 0 DO
      J 4 lshift I +
      J #ranks * I + stock +
      c!
    LOOP
  LOOP ;

: shuffle-stock
  stock #cards cshuffle ;

: pop-stock
  stock #dealt @ + c@
  1 #dealt +! ;

: card>rank $0F and ;
: card>suit 4 rshift ;

: =suit ( c c -- f )
  card>suit swap card>suit = ;

: <rank ( c c -- f )
  card>rank swap card>rank swap < ;

: .rank card>rank 12 + emit ;
: .suit card>suit 1+   emit ;
: .card dup .rank .suit ;

: .ui
  \ stock
  14 2 at-xy 34 emit 35 emit
  14 3 at-xy 38 emit 39 emit

  \ foundation bottom half
  14 8 at-xy 8 emit 9 emit ;

: .pile-1
  pile-1 size IF
    1 pile-1 size 1 + at-xy pile-1 peek .card
    1 pile-1 size 2 + at-xy 8 emit 9 emit
  ELSE
    1 pile-1 size 1 + at-xy BL emit BL emit
    1 pile-1 size 2 + at-xy BL emit BL emit
  THEN
  1 pile-1 size 3 + at-xy BL emit BL emit ;

: .pile-2
  pile-2 size IF
    4 pile-2 size 1 + at-xy pile-2 peek .card
    4 pile-2 size 2 + at-xy 8 emit 9 emit
  ELSE
    4 pile-2 size 1 + at-xy BL emit BL emit
    4 pile-2 size 2 + at-xy BL emit BL emit
  THEN
  4 pile-2 size 3 + at-xy BL emit BL emit ;

: .pile-3
  pile-3 size IF
    7 pile-3 size 1 + at-xy pile-3 peek .card
    7 pile-3 size 2 + at-xy 8 emit 9 emit
  ELSE
    7 pile-3 size 1 + at-xy BL emit BL emit
    7 pile-3 size 2 + at-xy BL emit BL emit
  THEN
  7 pile-3 size 3 + at-xy BL emit BL emit ;

: .pile-4
  pile-4 size IF
    10 pile-4 size 1 + at-xy pile-4 peek .card
    10 pile-4 size 2 + at-xy 8 emit 9 emit
  ELSE
    10 pile-4 size 1 + at-xy BL emit BL emit
    10 pile-4 size 2 + at-xy BL emit BL emit
  THEN
  10 pile-4 size 3 + at-xy BL emit BL emit ;

: .tableau .pile-1 .pile-2 .pile-3 .pile-4 ;

: show-hand
  26 hand1 spr-tile
  27 hand2 spr-tile
  28 hand3 spr-tile
  29 hand4 spr-tile

  lcd-wait-vblank
  c! c! c! c! ;

: hide-hand
  BL hand1 spr-tile
  BL hand2 spr-tile
  BL hand3 spr-tile
  BL hand4 spr-tile

  lcd-wait-vblank
  c! c! c! c! ;

: ix>pile
  case
    0 of pile-1 endof
    1 of pile-2 endof
    2 of pile-3 endof
    3 of pile-4 endof
  endcase ;

value hand-x
value hand-y
: .hand
  hand-ix @             24 * 16 + to hand-x
  hand-ix @ ix>pile size 8 * 32 + to hand-y

  hand-x     hand1 spr-x
  hand-x 8 + hand2 spr-x
  hand-x     hand3 spr-x
  hand-x 8 + hand4 spr-x

  hand-y     hand1 spr-y
  hand-y     hand2 spr-y
  hand-y 8 + hand3 spr-y
  hand-y 8 + hand4 spr-y

  lcd-wait-vblank
  c! c! c! c!
  c! c! c! c! ;

: .stock
  16 3 at-xy #cards #dealt @ - 3 .r ;

: .score
  14 7 at-xy foundation .card
  16 8 at-xy score @ 3 .r ;

: all-unique-tops? ( -- flag )
  pile-1 peek pile-2 peek =suit if false exit then
  pile-1 peek pile-3 peek =suit if false exit then
  pile-1 peek pile-4 peek =suit if false exit then

  pile-2 peek pile-3 peek =suit if false exit then
  pile-2 peek pile-4 peek =suit if false exit then

  pile-3 peek pile-4 peek =suit if false exit then
  true ;

: discard ( card -- )
  dup TO foundation
  card>rank 2 + score +! ;

: deal
  pop-stock pile-1 push
  pop-stock pile-2 push
  pop-stock pile-3 push
  pop-stock pile-4 push ;

: try-deal
  #cards #dealt @ - 0 > if
    deal
    .stock
    blip .pile-1 50 ms
    blip .pile-2 50 ms
    blip .pile-3 50 ms
    blip .pile-4
    .hand
  else
    buzz
  then ;

: move-card
  \ selected space is empty
  hand-ix @ ix>pile empty? if buzz exit then

  \ discard if higher rank of suit exists
  hand-ix @ ix>pile peek
  4 0 DO
    dup
    I ix>pile peek 2dup
    =suit IF
      <rank IF
        hand-ix @ ix>pile pop discard
        blip
        .tableau .hand .score
        drop unloop exit
      THEN
    ELSE 2drop THEN
  LOOP
  drop

  \ move to empty space
  4 0 DO
    I ix>pile empty? if
      hand-ix @ ix>pile pop
      I ix>pile push
      thud
      .tableau .hand
      unloop exit
    then
  LOOP

  \ no possible move
  buzz ;

: game-loop
  .ui
  .score
  .stock
  blip .pile-1 50 ms
  blip .pile-2 50 ms
  blip .pile-3 50 ms
  blip .pile-4
  .hand
  show-hand
  BEGIN
    \ no more cards to deal?
    #cards #dealt @ - 0 = if
      \ no more moves to make?
      all-unique-tops? if
        hide-hand
        exit \ game over!
      then
    then

    key CASE
      k-left  OF hand-ix @ 3 + 4 mod hand-ix ! .hand ENDOF
      k-right OF hand-ix @ 1+  4 mod hand-ix ! .hand ENDOF
      k-a     OF try-deal  ENDOF
      k-b     OF move-card ENDOF
    ENDCASE
  AGAIN ;

: .title
  6  6 at-xy ." Aces Up"

  4  8 at-xy 24 emit 1 emit
  4  9 at-xy  8 emit 9 emit

  7  8 at-xy 24 emit 2 emit
  7  9 at-xy  8 emit 9 emit

  10 8 at-xy 24 emit 3 emit
  10 9 at-xy  8 emit 9 emit

  13 8 at-xy 24 emit 4 emit
  13 9 at-xy  8 emit 9 emit

  4 11 at-xy ." Press Start" ;

: .gameover
  3 6 at-xy 25 emit 12 0 ?DO 30 emit LOOP 5 emit
  3 7 at-xy  6 emit    ." Final Score:"   7 emit
  3 8 at-xy  6 emit        12 spaces      7 emit
  3 9 at-xy  6 emit score @ 7 .r 5 spaces 7 emit
  3 10 at-xy 8 emit 12 0 ?DO 31 emit LOOP 9 emit ;

value explode-x
value explode-y
: explode
  0 DO
    [ SCRN_X_B 3 - ]L random 1+ to explode-x
    [ SCRN_Y_B 3 - ]L random 1+ to explode-y

    explode-x    explode-y    at-xy 34 emit
    explode-x 1+ explode-y    at-xy 35 emit
    explode-x    explode-y 1+ at-xy 38 emit
    explode-x 1+ explode-y 1+ at-xy 39 emit
  LOOP ;

: reset-game
  \ reset score, cursor, pile sizes
  0 score !
  0 hand-ix !
  0 pile-1 !
  0 pile-2 !
  0 pile-3 !
  0 pile-4 !

  \ foundation card is empty suit and rank
  $4D TO foundation

  \ shuffle the stock
  shuffle-stock
  0 #dealt !

  \ deal the initial 4 cards
  deal ;

: wait-for-key ( -- ) KEY drop ;

: wait-for-start ( -- )
  BEGIN KEY k-start = UNTIL ;

: main
  install-card-tiles
  init-term
  init-input
  init-sfx
  init-sprites
  init-stock
  $4ACE seed !
  BEGIN
    page .title wait-for-start page

    rnd utime xor seed !
    reset-game

    game-loop

    50 ms whoosh 200 ms
    52 explode
    .gameover wait-for-key
  AGAIN ;
