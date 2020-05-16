: test-1
  0
  begin 1+ dup 20 < until ;

: test-2
  0
  begin 1+ dup 20 = until ;


: main
  test-1
  test-2 ;
