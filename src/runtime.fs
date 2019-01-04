require ./asm.fs
require ./ram.fs
include ../shared/runtime.fs

[asm]

: sp-init,
  SP0 $FF00 - # C ld, ;

: rp-init,
  RP0 # SP ld, ;

: runtime-init,
  sp-init,
  rp-init,
  cp-init, ;

[endasm]
