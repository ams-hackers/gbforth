: test-1 42 >r r> ;
: test-2 0 >r rdrop 43 ;
: test-3 44 >r r@ rdrop ;

: test-4
  1 2 >r >r 2rdrop 45 ;

: test-5
  46 47 2>r 2r> ;

: test-6
  48 1111 2>r rdrop r> ;

: test-7
  49 50 2>r 2r@ 2rdrop ;

: main
  test-1
  test-2
  test-3
  test-4
  test-5
  test-6
  test-7
;
