require ./asm.fs

also gb-assembler

:noname
  assert( ~r  ~r    type-match )
  assert( ~n  ~n/8  type-match )
  assert( ~n  ~n/16 type-match invert )
  assert( ~nn ~n/8  type-match )
  assert( ~nn ~n/16 type-match )
  ." Test passed" ;
execute
