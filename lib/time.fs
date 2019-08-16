\ Highly inaccurate time related words

require gbhw.fs

: nops ;
: 4nops nops nops nops nops ;
: 8nops 4nops 4nops ;
: 16nops 8nops 8nops ;

: ms ( delay -- )
  0 DO 16nops 16nops LOOP ;

\ At 16384 Hz every tick is ~61 microseconds
: utime ( -- n )
  rDIV c@ 61 * ;
