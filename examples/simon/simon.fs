title: SIMON
makercode: TK

require gbhw.fs
require ibm-font.fs
require term.fs
require input.fs
require music.fs
include random.fs
include time.fs

value pattern-length
create pattern 20 cells allot

: pattern-clear ( -- )
  0 to pattern-length ;

: pattern-append ( n -- )
  pattern pattern-length cells + !
  pattern-length 1+ to pattern-length ;

: pattern-at ( n -- n )
  cells pattern + @ ;

\ font mapping to display joypad keys on screen
24 constant char-up
25 constant char-down
26 constant char-right
27 constant char-left

: show-note ( n -- )
  9 8 at-xy
  case
    E5  of char-up    emit endof
    C#5 of char-right emit endof
    A5  of char-down  emit endof
    E6  of char-left  emit endof
  endcase ;

: clear-note
  9 8 at-xy BL emit ;

: key>note ( -- n )
  BEGIN
    key case
      k-up    of E5  true endof
      k-right of C#5 true endof
      k-down  of A5  true endof
      k-left  of E6  true endof
      false swap
    endcase
  UNTIL ;

: random-note ( -- n )
  4 random case
    0 of E5  endof
    1 of C#5 endof
    2 of A5  endof
    3 of E6  endof
  endcase ;

: len>score ( n -- n ) dup 1+ * 2/ ;
: .score ( -- )
  8 0 at-xy
  pattern-length len>score
  5 .r space ." points" ;

: play-game ( -- )
  pattern-clear
  BEGIN
    \ display score
    .score

    \ wait a bit before starting
    1000 ms

    \ add note to pattern
    random-note pattern-append

    \ play pattern
    pattern-length 0 DO
      I pattern-at dup
      show-note
      note
      500 ms clear-note 50 ms
    LOOP

    \ user inputs the pattern
    pattern-length 0 DO
      key>note dup dup
      show-note
      note
      I pattern-at <> IF
        200 ms
        unloop exit
      ENDIF
    LOOP
    500 ms clear-note
  AGAIN ;

: wait-for-start ( -- )
  BEGIN KEY k-start = UNTIL ;

: .joy ( -- )
  2 4 at-xy 14 emit
  12 11 at-xy 13 emit
  10 2 at-xy 13 emit
  16 15 at-xy 14 emit
  6 13 at-xy 13 emit ;

: title-screen ( -- )
  page
  7 7 at-xy ." SIMON"
  4 8 at-xy ." press start"
  .joy
  BEGIN
    wait-for-start
    rDIV c@ 1 max seed !
    page play-game page
    .score
    .joy
    5 7 at-xy ." GAME OVER"
    300 ms
    E5 F5 G5 A5 B5 C6 D6 E6 8 0 DO note 120 ms LOOP
    600 ms
    4 7 at-xy ." PLAY AGAIN?"
    4 8 at-xy ." press start"
  AGAIN ;

: main
  install-font
  init-term
  init-input
  title-screen ;
