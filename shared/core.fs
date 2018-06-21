
: aligned ( x -- x )
  dup #2 mod if
    1+
  then ;

: align ( -- )
  here #2 mod if
    #1 allot
  then ;
