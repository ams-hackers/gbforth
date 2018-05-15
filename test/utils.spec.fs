require ../src/utils.fs

:noname
  assert( #4 kB #4096 = )
  assert( $ABCD lower-byte $CD = )
  assert( $ABCD higher-byte $AB = )
; execute

." OK" CR
bye
