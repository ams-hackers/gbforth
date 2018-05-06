
: lower-byte ( n1n2 -- n2 )
  $ff and ;

: higher-byte ( n1n2 -- n1 )
  $8 rshift ;
