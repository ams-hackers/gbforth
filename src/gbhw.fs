require ./asm.fs
also gb-assembler

( Special registers! )
: [rGBP] $FF47 ]* ;
: [rSCY] $FF42 ]* ;
: [rSCX] $FF43 ]* ;

previous
