require ../ram.fs
require ../rom.fs

( 0 if we have selected ROM,
  -1 if we have selected RAM )
variable memspace
: ram? memspace @ ;

: ROM 0 memspace ! ;
: RAM -1 memspace ! ;

\ Default to RAM
RAM

: ram-here ram-offset ;
: ram-unused ram-size ram-here CP0 - - ;
: ram-allot ram-offset+! ;

: rom-here rom-offset ;
: rom-unused rom-size rom-here - ;
: rom-allot rom-offset+! ;

: xhere   ram? if ram-here   else rom-here   then ;
: xunused ram? if ram-unused else rom-unused then ;
: xallot  ram? if ram-allot  else rom-allot  then ;
