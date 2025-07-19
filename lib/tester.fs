\ (C) 1995 JOHNS HOPKINS UNIVERSITY / APPLIED PHYSICS LABORATORY
\ MAY BE DISTRIBUTED FREELY AS LONG AS THIS COPYRIGHT NOTICE REMAINS.
\
\ Based on the original testing harnass by John Hayes,
\ modified to work properly with gbforth.

ROM
VARIABLE error-xt
: error error-xt @ execute ;
:noname 2drop bye ; error-xt !

RAM
VARIABLE actual-depth
CREATE   actual-results 32 cells allot
VARIABLE start-depth

: empty-stack ( ... -- )
  depth start-depth @ < IF clearstack THEN
  depth start-depth @ > IF
    depth start-depth @ DO drop LOOP
  THEN ;

: T{ ( -- )
  depth start-depth ! ;

: -> ( ... -- )
  depth dup actual-depth !
  start-depth @ > IF
    depth start-depth @ - 0 DO
      actual-results I cells + !
    LOOP
  THEN ;

: }T ( ... -- )
   depth actual-depth @ = IF
      depth start-depth @ > IF
         depth start-depth @ - 0 DO
           actual-results I cells + @
           <> IF S" INCORRECT RESULT" ERROR LEAVE THEN
         LOOP
      THEN
   ELSE
      S" WRONG NUMBER OF RESULTS" ERROR
   THEN empty-stack ;
