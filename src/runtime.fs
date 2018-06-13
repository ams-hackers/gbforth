( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs
require ./utils/bytes.fs

[asm]


( Helper words for stack manipulation )

$FFFE constant SP0 \ end of HRAM
$CFFF constant RS0 \ end of RAM bank 0
$C000 constant DP0 \ start of RAM bank 0

variable DP
DP0 2 + DP ! \ init dictionary pointer

: dp-init,
  DP @ dup
  lower-byte # A ld, A DP0 ]* ld,
  higher-byte # A ld, A DP0 1 + ]* ld, ;

: ps-clear,
  SP0 $FF00 - # C ld, ;

: ps-init,
  ps-clear,
  RS0 # SP ld, ;


[endasm]
