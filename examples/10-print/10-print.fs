title: 10-PRINT

require gbhw.fs
require random.fs
require term.fs
require ./petscii.fs

: print 205 2 random + emit ;

: main
  init-term
  _SCRN0 [ SCRN_VX_B SCRN_VY_B * ]L erase
  install-petscii
  1234 seed !
  BEGIN
    0 0 at-xy
    SCRN_Y_B 0 DO
      SCRN_X_B 0 DO
        print
      LOOP cr
    LOOP
  AGAIN ;
