#! /usr/bin/env gforth
(
dmgforth.fs ---
)

vocabulary dmgforth
dmgforth definitions

: kB 1024 * ;

32 kB constant rom-size
create rom-base rom-size allot
variable rom-offset-variable

: rom-offset rom-offset-variable @ ;
: rom-offset! rom-offset-variable ! ;
: rom-offset+! rom-offset-variable +! ;

: rom rom-base rom-size ;

: rom,
  rom-base rom-offset + c!
  $1 rom-offset+! ;

: logo
  $ce rom, $ed rom, $66 rom, $66 rom, $cc rom, $0d rom, $00 rom, $0b rom,
  $03 rom, $73 rom, $00 rom, $83 rom, $00 rom, $0c rom, $00 rom, $0d rom,
  $00 rom, $08 rom, $11 rom, $1f rom, $88 rom, $89 rom, $00 rom, $0e rom,
  $dc rom, $cc rom, $6e rom, $e6 rom, $dd rom, $dd rom, $d9 rom, $99 rom,
  $bb rom, $bb rom, $67 rom, $63 rom, $6e rom, $0e rom, $ec rom, $cc rom,
  $dd rom, $dc rom, $99 rom, $9f rom, $bb rom, $b9 rom, $33 rom, $3e rom, ;

: title
  $45 rom, $58 rom, $41 rom, $4d rom, $50 rom, $4c rom, $45 rom, $00 rom,
  $00 rom, $00 rom, $00 rom, $00 rom, $00 rom, $00 rom, $00 rom, $00 rom, ;

: gbgame $00 rom, ; ( non color )

: nop $0 rom, ;

rom erase

include rom.fs

0 Value rom-fd
: dump-rom ( c-addr u -- )
  s" ./output.gb" w/o bin create-file throw TO rom-fd
  rom rom-fd write-file throw
  rom-fd close-file throw ;

\ main

dump-rom
." Generated file output.gb" cr

bye
