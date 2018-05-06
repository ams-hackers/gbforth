#! /usr/bin/env gforth
(
dmgforth.fs ---
)

VOCABULARY DMGFORTH
ALSO DMGFORTH DEFINITIONS


: kB 1024 * ;

32 kB constant rom-size
create rom-base rom-size allot
variable rom-offset-variable

: rom-offset rom-offset-variable @ ;
: rom-offset! rom-offset-variable ! ;
: rom-offset+! rom-offset-variable +! ;

: rom rom-base rom-size ;

: offset>addr
  rom-base + ;

: rom@ ( offset -- val )
  offset>addr c@ ;

: rom! ( val offset -- )
  offset>addr c! ;

: rom,
  rom-offset rom!
  $1 rom-offset+! ;

: ==> ( n -- )
  rom-offset! ;

: logo
  $ce rom, $ed rom, $66 rom, $66 rom, $cc rom, $0d rom, $00 rom, $0b rom,
  $03 rom, $73 rom, $00 rom, $83 rom, $00 rom, $0c rom, $00 rom, $0d rom,
  $00 rom, $08 rom, $11 rom, $1f rom, $88 rom, $89 rom, $00 rom, $0e rom,
  $dc rom, $cc rom, $6e rom, $e6 rom, $dd rom, $dd rom, $d9 rom, $99 rom,
  $bb rom, $bb rom, $67 rom, $63 rom, $6e rom, $0e rom, $ec rom, $cc rom,
  $dd rom, $dc rom, $99 rom, $9f rom, $bb rom, $b9 rom, $33 rom, $3e rom, ;

: parse-line ( -- addr u )
  #10 parse ;

: title:
  parse-line
  dup #15 > abort" Title is too long"
  $134 offset>addr swap move ;

: manufacturer:
  parse-line
  dup #4 > abort" Manufacturer Code is too long"
  $013F offset>addr swap move ;

: licensee:
  parse-line
  dup #2 > abort" Licensee Code is too long"
  $0144 offset>addr swap move
  $33 $014B rom! ;

: gbgame $00 $0143 rom! ; ( non color )

require ./utils.fs
require ./asm.fs

rom erase
include ./rom.fs

: header-complement
  0
  $014D $0134 ?do
    i rom@ +
  loop
  $19 + negate ;

: fix-header-complement
  header-complement $014D rom! ;

0 Value rom-fd
: dump-rom ( c-addr u -- )
  s" ./output.gb" w/o bin create-file throw TO rom-fd
  rom rom-fd write-file throw
  rom-fd close-file throw ;


fix-header-complement

dump-rom
." Generated file output.gb" cr

bye
