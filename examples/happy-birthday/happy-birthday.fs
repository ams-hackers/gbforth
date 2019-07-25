title: HAPPYBIRTHDAY
gamecode: GB30

require ibm-font.fs
require term.fs
require input.fs
require music.fs

: nothing ;
: wait ( delay -- )
  10 * 0 DO
   nothing
  LOOP ;
: play ( freq -- ) note 50 wait ;

: wait-for-key KEY drop ;
: wait-for-start
  BEGIN KEY k-start = UNTIL ;

: **********
  20 0 DO ." *" 2 wait LOOP CR ;

: sing
  ."    "  ." Hap"   G5 play ." py "  G5 play ." birth"  A5 play ." day " G5 play  CR
  ."    "  ." to "   C6 play ." you"  B5 play                                      CR
  90 wait CR
  ."    "  ." Hap"   G5 play ." py "  G5 play ." birth"  A5 play ." day " G5 play  CR
  ."    "  ." to "   D6 play ." you"  C6 play                                      CR
  90 wait CR
  ."    "  ." Hap"   G5 play ." py "  G5 play ." birth"  G6 play ." day " E6 play  CR
  ."    "  ." dear " C6 play ." Game" B5 play ."  Boy!"  A5 play                   CR
  130 wait CR
  ."    "  ." Hap"   F6 play ." py "  F6 play ." birth"  E6 play ." day " C6 play  CR
  ."    "  ." to "   D6 play ." you"  C6 play                                      CR ;

: .counter ( from to )
  1+ swap dup 1+ swap
  8 8 at-xy . wait-for-key
  DO
    8 8 at-xy
    I dup 1985 - 40 * swap
    . play
  LOOP
  120 wait ;

: start
  1989 2019 .counter

  BEGIN
  page
  0 1 at-xy
  **********
  CR sing CR
  **********
  wait-for-start AGAIN ;

: main
  install-font
  init-term
  init-input
  start ;
