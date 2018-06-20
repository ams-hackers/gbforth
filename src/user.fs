( User Vocabulary )

require ./runtime.fs
require ./vocabulary.fs
require ./cartridge.fs
require ./header.fs
require ./asm.fs
require ./rom.fs
require ./compiler/cross.fs

: export
  parse-name
  2dup find-name name>int >r
  nextname r> alias ;

: [user-definitions]
  get-current
  also gbforth-user definitions ;

: [end-user-definitions]
  previous set-current ;

: create-constant ( x c-addr u  -- )
  2dup >r >r nextname
  dup constant

  r> r> nextname
  xconstant ;

[user-definitions]
also gbforth

: [host] forth ; immediate
: [target] gbforth-user ; immediate

: ! rom! ;
: c! romc! ;
: , rom, ;
: c, romc, ;
: s" rom" ;

export ( immediate
export \ immediate
export ==>
export include
export require
export [asm]
export [endasm]

export main:

export title:
export gamecode:
export makercode:

export code
export -end-code immediate
export end-code immediate

export dup
export over
export drop
export rot
export nip
export +
export -
export *
export /
export or
export xor
export and
export invert
export lshift
export rshift

[asm]
: execute # call, ;
[endasm]

: constant ( x -- )
  parse-next-name create-constant ;

: constant-sym ( x -- )
  >r
  parse-next-name
  2dup r@ sym
  r> -rot create-constant ;

: here rom-offset ;
: unused rom-size here - ;
: create here constant-sym ;
: cells $2 * ;
: allot rom-offset+! ;

: ram-here CP @ ;
: ram-create ram-here constant-sym ;
: ram-allot CP +! ;

: variable
  ram-create
  $2 ram-allot ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;

previous
[end-user-definitions]
