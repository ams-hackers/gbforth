#! /usr/bin/env gforth
(
dmgforth.fs ---
)

VOCABULARY DMGFORTH
ALSO DMGFORTH DEFINITIONS

: output-file s" ./output.gb" ;

require ./rom.fs

include ./game.fs

fix-header-complement
output-file dump-rom
." Generated file " output-file type cr

bye
