: test-1
  10 0 ?do i loop 99 ;

: test-2
  5 0 ?do
    i
    i 2 = if unloop exit then
  loop
  99 ;

: test-3
  5 0 ?do
    i
    i 2 = if leave then
  loop
  99 ;

: main
  test-1
  test-2
  test-3
;
