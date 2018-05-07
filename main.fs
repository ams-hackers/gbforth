#! /usr/bin/env gforth

require ./src/dmgforth.fs
also dmgforth

: output-file s" output.gb" ;

rom erase

include src/game.fs

fix-header-complement
output-file dump-rom
." Generated file " output-file type cr
bye
