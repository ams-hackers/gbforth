( User Vocabulary )

require ./vocabulary.fs
require ./cartridge.fs
require ./asm.fs
require ./rom.fs
require ./compiler/cross.fs

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
export include
export require
export [asm]
export [endasm]

export __start:
export main:

export title:
export gamecode:
export makercode:

export code
export end-code

( TODO: Remove me! )
export push-lit,

: execute # call, ;

: constant
  >r
  parse-next-name
  2dup nextname r@ constant

  nextname
  r> xconstant ;

: variable
  CP @
  $2 CP +!
  constant ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;

previous
[end-user-definitions]
