require ./asm.fs
require ./utils/bytes.fs

include ../shared/dictionary.fs

usable-dictionary-end DP0 - constant ram-size

variable ram-offset-variable
DP0 ram-offset-variable !

: ram-offset ram-offset-variable @ ;
: ram-offset+! ram-offset-variable +! ;

[asm]

: cp-init,
  ram-offset dup
  lower-byte # A ld, A DP ]* ld,
  higher-byte # A ld, A DP 1 + ]* ld, ;

[endasm]
