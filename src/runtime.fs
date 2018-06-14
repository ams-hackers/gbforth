( A SUBROUTINE-THREADED KERNEL FOR GBFORTH )

require ./asm.fs
require ./utils/bytes.fs

[asm]

$FFFE constant SP0 \ end of HRAM
$CFFF constant RP0 \ end of RAM bank 0
$C002 constant CP0 \ start of RAM bank 0
$C000 constant CP-addr

variable CP
CP0 CP ! \ init compiler pointer

: cp-init,
  CP @ dup
  lower-byte # A ld, A CP-addr ]* ld,
  higher-byte # A ld, A CP-addr 1 + ]* ld, ;

: sp-init,
  SP0 $FF00 - # C ld, ;

: rp-init,
  RP0 # SP ld, ;

: runtime-init,
  sp-init,
  rp-init,
  cp-init, ;

[endasm]
