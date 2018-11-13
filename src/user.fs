( User Vocabulary )

require ./runtime.fs
require ./vocabulary.fs
require ./cartridge.fs
require ./header.fs
require ./asm.fs
require ./compiler/xmemory.fs
require ./compiler/xname.fs
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

: assert-rom-selected ( -- )
  ram? abort" Unavailable when RAM is selected" ;

: [host] forth ; immediate
: [target] gbforth-user ; immediate

\
\ Expose words into the standard FORTH vocabulary. Available within
\ [HOST] in interpreting mode. Keep this list small!
\
get-current
also Forth definitions

export rom-move

previous
set-current

\
\ Expose words into the GBFORTH-USER vocabulary. Available within
\ [TARGET] in interpreting mode.
\
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

export rom
export ram

: (s") rom" ;

: here xhere ;
: unused xunused ;
: allot xallot ;

: create-host ( here c-addr u -- )
  nextname constant ;

: create-target ( here c-addr u -- )
  make-ir { ir }
  ir to current-node
  rot xliteral,
  xreturn,
  -1 to current-node

  ir finalize-ir

  nextname
  ir 0 create-xname ;

: create
  xhere
  parse-next-name
  { here c-addr u }
  here c-addr u create-host
  latestxt
    here c-addr u create-target 
  xlatest >xhost!
;

: (replace-with-does) ( does-ir -- )
  xlatest xregular? invert if
    true abort" DOES> can only be used with words created with CREATE"
  then
  make-ir { ir }
  ir to current-node
  xlatest >xdfa xliteral,
  ( does-ir ) branch,
  -1 to current-node
  ir finalize-ir
  ir xlatest >xcode! ;

: does>
  postpone [
  xdoes: 
  ]L
  postpone (replace-with-does)
  postpone ;
; immediate


: :m : ;

: ; xcompiling? if x; else postpone ; then ; immediate
latestxt F_IMMEDIATE create-xname ;

: postpone xname' xpostpone, ; immediate

: constant ( x -- )
  parse-next-name create-constant ;

: variable create $2 allot ;

: @ rom@ ;
: c@ romc@ ;
: ! rom! ;
: c! romc! ;
: , assert-rom-selected rom, ;
: c, assert-rom-selected romc, ;
: +! dup rom@ rot + swap rom! ;

: fill ( offset u c -- )
  rot <rom -rot fill ;
: erase 0 fill ;

: chars ( $1 * ) ;
: char+ 1+ ;
: cells $2 * ;
: cell+ $2 + ;

: 'body x'body ;

include ../shared/core.fs

: immediate ximmediate ;

: ' x' ;
: ] x] ;
: ]L xliteral x] ;
: :noname x:noname ;

: : x: ;


previous
[end-user-definitions]
