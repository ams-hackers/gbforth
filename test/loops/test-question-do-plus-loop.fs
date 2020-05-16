: test-1
  10 0 ?do i 3 +loop ;

: test-2
  -1 0 ?do  i -1 +loop ;

: test-3
  0 0 ?do  i  -1 +loop ;

: main
  test-1
  test-2
  test-3
;
