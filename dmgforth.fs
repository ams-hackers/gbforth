#! /usr/bin/env gforth
( 
dmgforth.fs --- 
)

vocabulary dmgforth
dmgforth definitions

: kB 1024 * ;

32 kB constant rom-size
create rom-base rom-size allot
: rom rom-base rom-size ;

0 Value rom-fd 
: dump-rom ( c-addr u -- )
  s" ./output.gb" w/o bin create-file throw TO rom-fd
  rom rom-fd write-file throw
  rom-fd close-file throw ;

rom erase

\ Next, the GameBoy starts adding all of the bytes
\ in the cartridge from $134 to $14d. A value of 25
\ decimal is added to this total. If the least
\ significant byte of the result is a not a zero,
\ then the GameBoy will stop doing anything

$ff rom-base 10 + c!

\ main

dump-rom
." Generated file output.gb" cr

bye
