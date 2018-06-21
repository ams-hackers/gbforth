: test-case ( n -- )
  case
    1 of 10 endof
    2 of 20 endof
    3 of 30 endof
    100 swap
  endcase ;

: main
  1 test-case
  2 test-case
  3 test-case
  9 test-case ;
