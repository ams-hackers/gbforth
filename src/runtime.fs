require ./asm.fs
require ./ram.fs

[asm]

$FFFE constant SP0 \ end of HRAM
$CFFF constant RP0 \ end of RAM bank 0

: sp-init,
  SP0 $FF00 - # C ld, ;

: rp-init,
  RP0 # SP ld, ;

: runtime-init,
  sp-init,
  rp-init,
  cp-init, ;

[endasm]
