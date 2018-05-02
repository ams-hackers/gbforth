#! /usr/bin/env gforth
(
dmgforth.fs ---
)

vocabulary dmgforth
dmgforth definitions

: kB 1024 * ;

32 kB constant rom-size
create rom-base




include rom.fs




: rom rom-base rom-size ;

0 Value rom-fd
: dump-rom ( c-addr u -- )
  s" ./output.gb" w/o bin create-file throw TO rom-fd
  rom rom-fd write-file throw
  rom-fd close-file throw ;

\ main

dump-rom
." Generated file output.gb" cr

bye
