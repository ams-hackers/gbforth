: .warn-line
  ( addr u ) >r >r
  input-error-data ( addr1 u1 addr2 u2 #line [fname-addr fname-u] )
  cr type ." :" ( addr1 u1 addr2 u2 #line )
  dup 0 dec.r ." : "
  r> r> ( addr u ) type ." :" cr
  ( #line ) if
    ( addr1 u1 addr2 u2 ) .error-line cr
  then ;
