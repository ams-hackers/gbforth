( User Vocabulary )

require ./cartridge.fs
require ./cross.fs
require ./asm.fs

: export
  parse-name
  2dup find-name name>int >r
  nextname r> alias ;

: [user-definitions]
  get-current
  also dmgforth-user definitions ;

: [end-user-definitions]
  previous set-current ;


[user-definitions]
also dmgforth

: [host] also forth ;
: [endhost] previous ;

: c, rom, ;

export (
export ==>
export \
export constant
export include
export require
export [asm]
export [endasm]

export __start
export __start:
export main
export main:

export title:
export gamecode:
export makercode:

( TODO: Remove me! )
export ps-push-lit,

: ' x' ;
: : x: ;

previous
[end-user-definitions]
