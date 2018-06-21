require ./asm.fs
require ./utils/bytes.fs

$C000 constant CP-addr \ start of RAM bank 0
$C002 constant CP0

$CFFF CP0 - constant ram-size

variable ram-offset-variable
CP0 ram-offset-variable !

: ram-offset ram-offset-variable @ ;
: ram-offset+! ram-offset-variable +! ;

[asm]

: cp-init,
  ram-offset dup
  lower-byte # A ld, A CP-addr ]* ld,
  higher-byte # A ld, A CP-addr 1 + ]* ld, ;

[endasm]
