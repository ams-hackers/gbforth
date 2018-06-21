
: aligned ( x -- x )
  dup %1 and + ;

: align ( -- )
  here %1 and if #1 allot then ;
