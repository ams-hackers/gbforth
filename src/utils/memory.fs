\ Like %allocate, but it zeroes the memory
: %zalloc ( align size -- addr )
  2dup %size >r
  %allocate throw
  dup r> erase ;
