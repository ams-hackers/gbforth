title: 10-PRINT
makercode: TK

require gbhw.fs
require random.fs
require term.fs
require ibm-font.fs

: random-line
  2 random IF
    [char] /
  ELSE
    [char] \
  THEN ;

: main
  install-font
  init-term
  1234 seed !
  BEGIN
    0 0 at-xy
    SCRN_Y_B 0 DO
      SCRN_X_B 0 DO
        random-line emit
      LOOP cr
    LOOP
  AGAIN ;
