require ./asm.fs
require ./ram.fs
include ../shared/runtime.fs

[asm]

: preserve-cgb-flag,
  A DP0 ]* ld, ;

: sp-init,
  SP0 $FF00 - # C ld, ;

: rp-init,
  RP0 # SP ld, ;

: runtime-init,
  preserve-cgb-flag,
  sp-init,
  rp-init,
  cp-init, ;

[endasm]
