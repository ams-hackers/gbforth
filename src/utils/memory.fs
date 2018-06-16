\ Like %allot, but it zeroes the memory 
: %zallot ( align size -- addr )
  2dup %size >r
  %allot
  dup r> erase ;

\ Like %allocate, but it zeroes the memory
: %zalloc ( align size -- addr )
  2dup %size >r
  %alloc
  dup r> erase ;
