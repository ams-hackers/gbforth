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

: ram-here ram-offset ;
: ram-unused ram-size ram-here - ;
: ram-create ram-here constant-sym ;
: ram-allot ram-offset+! ;

: rom-here rom-offset ;
: rom-unused rom-size rom-here - ;
: rom-create rom-here constant-sym ;
: rom-allot rom-offset+! ;

: between ( n min max -- f )
  -rot over <=
  -rot >=
  and ;

: assert-rom-addr ( addr -- )
  dup $C000 $DFFF between abort" Trying to reference RAM address"
  dup $0000 $7FFF between invert abort" Trying to reference an address outside ROM" ;

: assert-rom-selected ( -- )
  ram? abort" Unavailable when RAM is selected" ;

[user-definitions]
also gbforth

: [host] forth ; immediate
: [target] gbforth-user ; immediate

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
export dup
export over
export drop
export rot
export nip
export pick
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
export 0=
export 1+
export 1-
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

: ?do    postpone ?do    ; immediate
: loop   postpone loop   ; immediate
: begin  postpone begin  ; immediate
: until  postpone until  ; immediate
: while  postpone while  ; immediate
: repeat postpone repeat ; immediate
: again  postpone again  ; immediate

: :m : ;

: ; xcompiling? if x; else postpone ; then ; immediate
latestxt F_IMMEDIATE create-xname ;

: postpone xname' xpostpone, ; immediate

: constant ( x -- )
  parse-next-name create-constant ;

: rom 0 memspace ! ;
: ram -1 memspace ! ;

: @ assert-rom-addr rom@ ;
: c@ assert-rom-addr romc@ ;
: ! assert-rom-addr rom! ;
: c! assert-rom-addr romc! ;
: , assert-rom-selected rom, ;
: c, assert-rom-selected romc, ;
: s" assert-rom-selected rom" ;

: here   ram? if ram-here   else rom-here   then ;
: unused ram? if ram-unused else rom-unused then ;
: allot  ram? if ram-allot  else rom-allot  then ;
: create ram? if ram-create else rom-create then ;

: chars ( $1 * ) ;
: char+ 1+ ;
: cells $2 * ;
: cell+ $2 + ;

: variable
  ram-create
  $2 ram-allot ;

: immediate ximmediate ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;

previous
[end-user-definitions]
