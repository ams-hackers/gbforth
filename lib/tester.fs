\ (C) 1995 JOHNS HOPKINS UNIVERSITY / APPLIED PHYSICS LABORATORY
\ MAY BE DISTRIBUTED FREELY AS LONG AS THIS COPYRIGHT NOTICE REMAINS.
\ VERSION 1.2
\ -------------------------------------------------------------------
\ Modified to work properly with gbforth

RAM
VARIABLE #errors
VARIABLE actual-depth
CREATE actual-stack 20 cells allot

: fail ( ... -- )
  #errors @ 1 + #errors ! ;

: T{ ( ... -- )
  clearstack ;

: -> ( ... -- )
   depth
   dup actual-depth !
   ?dup IF
      0 DO actual-stack I cells + ! LOOP
   THEN ;

: }T ( ... -- )
   depth actual-depth @ = IF
      depth ?dup IF
         0 DO
           actual-stack I cells + @
           = 0= IF fail LEAVE THEN
         LOOP
      THEN
   ELSE
      fail
   THEN clearstack ;

: init-tester
  0 #errors ! ;
