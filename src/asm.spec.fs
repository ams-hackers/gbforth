require ./asm.fs

also gb-assembler-impl
also gb-assembler

: istype
  arg1-type type-match flush-args ;

:noname
  assert( A       ~r  istype )
  assert( $0 #    ~n  istype )
  assert( $0 #    ~nn istype )
  assert( $FFFF # ~n  istype invert )
  assert( $FFFF # ~nn istype )
  ." Test passed" CR ;
execute
