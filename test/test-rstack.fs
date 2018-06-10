[asm]

: test-1 42 >r r> ;
: test-2 0 >r rdrop 43 ;
: test-3 44 >r r@ rdrop ;

:noname
  test-1
  test-2
  test-3
;

main: execute

begin, halt, repeat,
[endasm]
