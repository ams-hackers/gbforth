( User Vocabulary )

require ./runtime.fs
require ./vocabulary.fs
require ./cartridge.fs
require ./header.fs
require ./asm.fs
require ./rom.fs
require ./ram.fs
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

: constant-sym ( x -- )
  >r
  parse-next-name
  2dup r@ sym
  r> -rot create-constant ;

( 0 if we have selected ROM,
  -1 if we have selected RAM )
variable memspace
: ram? memspace @ ;
: rom 0 memspace ! ;
: ram -1 memspace ! ;

\ Default to RAM
RAM

: ram-here ram-offset ;
: ram-unused ram-size ram-here - ;
: ram-create ram-here constant-sym ;
: ram-allot ram-offset+! ;

: rom-here rom-offset ;
: rom-unused rom-size rom-here - ;
: rom-create rom-here constant-sym ;
: rom-allot rom-offset+! ;

: assert-rom-addr ( addr -- )
  dup $C000 $DFFF within abort" Trying to reference RAM address"
  dup $0000 $7FFF within invert abort" Trying to reference an address outside ROM" ;

: assert-rom-selected ( -- )
  ram? abort" Unavailable when RAM is selected" ;

: [host] forth ; immediate
: [target] gbforth-user ; immediate

[user-definitions]
also gbforth

' [host]   alias [host]   immediate
' [target] alias [target] immediate

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

export swap
export over
export dup
export tuck
export ?dup
export drop
export rot
export nip
export pick
export 2swap
export 2over
export 2dup
export 2drop
export +
export -
export *
export /
export 2*
export 2/
export <
export >
export u<
export u>
export =
export <>
export mod
export or
export xor
export and
export invert
export lshift
export rshift
export 0=
export 0<>
export 0<
export 1+
export 1-
export negate
export quit
export bye

export >r
export r>
export r@
export rdrop
export 2>r
export 2r>

export cr
export .s
export order
export words

export @there
export @resolve
export branch,
export 0branch,

: :m : ;

: ; xcompiling? if x; else postpone ; then ; immediate
latestxt F_IMMEDIATE create-xname ;

: postpone xname' xpostpone, ; immediate

: constant ( x -- )
  parse-next-name create-constant ;

export rom
export ram

: (s") rom" ;

: here   ram? if ram-here   else rom-here   then ;
: unused ram? if ram-unused else rom-unused then ;
: allot  ram? if ram-allot  else rom-allot  then ;
: create ram? if ram-create else rom-create then ;

: variable create $2 allot ;

: @ assert-rom-addr rom@ ;
: c@ assert-rom-addr romc@ ;
: ! assert-rom-addr rom! ;
: c! assert-rom-addr romc! ;
: , assert-rom-selected rom, ;
: c, assert-rom-selected romc, ;
: +! assert-rom-addr dup rom@ rot + swap rom! ;

: chars ( $1 * ) ;
: char+ 1+ ;
: cells $2 * ;
: cell+ $2 + ;

include ../shared/core.fs

: immediate ximmediate ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;

previous
[end-user-definitions]
