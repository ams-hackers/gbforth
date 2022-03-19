(
  ***** Constants *****
)

#32 constant bl

$0000 constant false
$FFFF constant true

#2 constant cell

include ../shared/runtime.fs

[asm]
:m [R1] $FF80 ]* ;
:m [R2] $FF81 ]* ;
[endasm]

(
  ***** Stack Manipulation *****
)

[host]
include ../shared/asm-utils.fs
[target]

code sp@
C D ld,
ps-dup,
$FF # H ld,
D L ld,
end-code

code clearstack
sp0 $FF00 - # C ld,
end-code

( x -- x x )
code dup
ps-dup,
end-code

code ?dup
H|L->A,
#nz if,
  ps-dup,
then,
end-code

( x -- )
code drop
ps-drop,
end-code

( x1 x2 -- x1 x2 )
code swap
ps-swap,
end-code

( x1 x2 -- x2 )
code nip
C inc,
C inc,
end-code

( x1 x2 -- x1 x2 x1 )
code over
ps-over,
end-code

( x1 x2 -- x2 x1 x2 )
code tuck
ps-swap,
ps-over,
end-code

(
  ***** Arithmetic *****
)

( n1 n2 -- n )
code +
ps-over-de-nip,
DE HL add,
end-code

( n1 n2 -- n )
code -
ps-pop-de,
L A ld,
E A sub,
A L ld,
H A ld,
D A sbc,
A H ld,
end-code

code * ( n1 n2 -- n )
ps-over-de-nip,
BC push,
H B ld,
L C ld,
$0 # HL ld,
$10 # A ld,
begin,
  HL HL add,
  E rl,
  D rl,
there> #NC jp,
  BC HL add,
there> #NC jp,
  DE inc,
>here >here
  A dec,
#Z until,
BC pop,
\ ps-push-de, ( DE contains higher 2 bytes of result )
end-code

( n1 n2 -- n )
code /
  ps-pop-de,      \ dividend to HL, divisor to DE
  BC push,        \ store SP
  HL/DE,
  B H ld, C L ld, \ move BC [quotient] to HL [TOS]
  BC pop,         \ restore SP
end-code

( n1 n2 -- n )
code mod
  ps-pop-de,  \ dividend to HL, modulus to DE
  BC push,
  HL/DE,
  BC pop,
end-code

code /mod
  ps-pop-de,
  BC push,
  HL/DE,
  \ quotient from BC -> DE
  B D ld,
  C E ld,
  BC pop,
  ps-push-de,
end-code


( n1 -- n2 )
code 1+
\ avoid using inc, because of OAM bug
$1 # DE ld,
DE HL add,
end-code

( n1 -- n2 )
code 1-
\ avoid uising dec, because of OAM bug
L A ld, $1 # A sub, A L ld,
H A ld, $0 # A sbc, A H ld,
end-code

(
  ***** Bitwise Operations *****
)

( x1 x2 -- x )
code and
ps-over-de-nip,
H A ld, D A and, A H ld,
L A ld, E A and, A L ld,
end-code

( x1 x2 -- x )
code or
ps-over-de-nip,
H A ld, D A or, A H ld,
L A ld, E A or, A L ld,
end-code

( x1 x2 -- x )
code xor
ps-over-de-nip,
H A ld, D A xor, A H ld,
L A ld, E A xor, A L ld,
end-code

( u1 n -- u2 )
code lshift
ps-pop-de,
begin,
  E A ld, $1 # A sub, A E ld,
  D A ld, $0 # A sbc, A D ld,
#C ret,
  HL HL add,
repeat,
-end-code

( u1 n -- u2 )
code rshift
ps-pop-de,
begin,
  E A ld, $1 # A sub, A E ld,
  D A ld, $0 # A sbc, A D ld,
#C ret,
  H srl,
  L rr,
repeat,
-end-code

( n1 -- n2 )
code 2*
HL HL add,
end-code

( n1 -- n2 )
code 2/
H sra,
L rr,
end-code

(
  ***** Numeric Comparison *****
)

( u1 u2 -- f )
code u<
ps-over-ae-nip, \ x -> ae
H A cp, \ compare higher byte
#Z if, \ x_=y_
  E A ld, L A cp, \ compare lower byte
then,
#C if, \ x<y
  true->HL,
else,
  false->HL,
then,
end-code

( u1 u2 -- f )
code u>
ps-over-ae-nip, \ x -> ae
H A cp, \ compare higher byte
#Z if, \ x_=y_
  E A ld, L A cp, \ compare lower byte
then,
#C if, \ x<y
  false->HL,
  ret,
then,
#Z if, \ x=y
  false->HL,
  ret,
then, \ x>y
true->HL,
end-code

( n1 n2 -- f )
code =
ps-over-ae-nip, \ x -> ae
\ compare higher byte
H A cp,
  there> #NZ jp, \ x<>y
\ compare lower byte
E A ld, L A cp,
  there> #NZ jp, \ x<>y

true->HL,
ret,

>here >here \ x<>y
false->HL,
end-code

code invert
  ps-invert,
end-code

code negate
  ps-negate,
end-code

code 0<>
  H|L->A,
  there> #z jr,
  -1 # HL ld,
  ret,
  >here
  0 # HL ld,
end-code

: <> = invert ;

code 0<
  H 7 # bit,
  there> #z jr,
  ( Positive )
  true->HL,
  ret,
  ( Negative )
  >here
  false->HL,
end-code

: < - 0< ;
: > swap < ;
: >= < invert ;
: <= > invert ;

(
  ***** Memory Access *****
)

( c-addr -- c )
code c@
[HL] L ld,
$0 # H ld,
end-code

( c c-addr -- )
code c!
ps-over-de-nip,
E [HL] ld,
ps-drop,
end-code

( a-addr -- x )
code @
[HL+] A ld,
[HL] H ld,
A L ld,
end-code

( x a-addr -- )
code !
ps-over-de-nip,
E A ld, A [HL+] ld,
D [HL] ld,
ps-drop,
end-code

( n a-addr -- )
code +!
[HL+] A ld, A E ld,
[HL] A ld, A D ld,
HL push,
ps-drop,
DE HL add,
H A ld, L E ld,
HL pop,
A [HL-] ld,
E [HL] ld,
ps-drop,
end-code

include ../shared/dictionary.fs

: here ( -- addr ) DP @ ;
: allot ( n -- ) DP +! ;
: unused ( -- n )
  usable-dictionary-end here - ;

: char+ ( c-addr1 -- c-addr2 )
  1+ ;

: chars ( n1 -- n2 )
  ( 1 * ) ;

: cell+ ( a-addr1 -- a-addr2 )
  cell + ;

: cells ( n1 -- n2 )
  cell * ;

( a-addr -- x2 x2 )
: 2@ dup cell+ @ swap @ ;

( x1 x2 a-addr -- )
: 2! swap over ! cell+ ! ;


: , ( x -- )
  here !
  cell allot ;

: c, ( c -- )
  here c!
  #1 allot ;

code execute
\ The stack contains the return address for execute. We put HL on top,
\ so RET will take it and jump there.
hl push,
( but just before that, we have to drop the address from the data stack )
ps-drop,
end-code

: depth sp@ sp0 swap - 2/ ;

require lcd.fs

( Tools )

code bye ( -- )
begin,
halt,
again,
end-code

code quit ( -- )
begin,
stop,
again,
end-code

( Return Stack manipulation )

code >r
DE pop,
HL push, ps-drop,
DE push,
end-code

code r>
ps-dup,
DE pop,
HL pop,
DE push,
end-code

code r@
ps-dup,
DE pop,
HL pop,
HL push,
DE push,
end-code

code rdrop
DE pop,
SP inc,
SP inc,
DE push,
end-code

code exit
\ Discard exit's return address so RET will
\ exit from the caller instaed
  DE pop,
end-code

[asm]
:m DE->R12,
  D ->A-> [R1] ld,
  E ->A-> [R2] ld, ;

:m R12->DE,
  [R1] ->A-> D ld,
  [R2] ->A-> E ld, ;
[endasm]

code 2>r
  DE pop,
  DE->R12,

  ps-over-de-nip,
  DE push,
  HL push,
  ps-drop,

  R12->DE,
  DE push,
end-code

code 2r>
  DE pop,
  DE->R12,

  ps-dup,
  DE pop,
  HL pop,
  ps-push-de,

  R12->DE,
  DE push,
end-code

code 2r@
  DE pop,
  DE->R12,

  ps-dup,

  HL pop,
  DE pop,

  C dec,
  D ->A-> [C] ld,
  C dec,
  E ->A-> [C] ld,

  DE push,
  HL push,

  R12->DE,
  DE push,
end-code

code 2rdrop
  DE pop,
  DE->R12,

  DE pop,
  DE pop,

  R12->DE,
  DE push,
end-code


code pick
HL HL add,                      ( HL*2 )
$ff00 # DE ld,                  ( ff00 )
DE HL add,
0 # B ld,                       ( C [= BC] )
BC HL add,

[HL+] ->A-> E ld,
[HL+] ->A-> D ld,

D H ld,
E L ld,
end-code

: rot ( x1 x2 x3 -- x2 x3 x1 )
  >r swap r> swap ;

: -rot ( x1 x2 x3 -- x3 x1 x2 )
  swap >r swap r> ;

: 2dup ( x1 x2 -- x1 x2 x1 x2 )
  over over ;

: 2drop ( x1 x2 -- )
  drop drop ;

: 2nip ( x1 x2 x3 x4 -- x3 x4 )
  >r >r drop drop r> r> ;

: 2over ( x1 x2 x3 x4 -- x1 x2 x3 x4 x1 x2 )
  >r >r over over r> -rot r> -rot ;

: 2swap ( x1 x2 x3 x4 -- x3 x4 x1 x2 )
  >r -rot r> -rot ;

: 2rot ( x1 x2 x3 x4 x5 x6 -- x3 x4 x5 x6 x1 x2 )
  >r >r >r -rot r> -rot r> -rot r> -rot ;

: 2tuck ( x1 x2 x3 x4 -- x3 x4 x1 x2 x3 x4 )
  >r dup >r -rot r> r> swap >r dup >r -rot r> r> swap ;

: 0= 0 = ;

:m to
  'body ( ' >body )
  postpone ! ; immediate

:m is to ; immediate

:m value create cell allot does> @ ;

:m defer create cell allot does> @ execute ;

:m 2constant create , , DOES> 2@ ;

\ HACK: As s", define two versions of the word to deal with the
\ USER/CROSS discrepancies.
:m [char]
  [host] char [target]
  postpone literal
; immediate

[host]
: [char] postpone [char] ; immediate
[target]

\ HACK: s" behaves differently in inteprreting and compiling mode. We
\ could define a "smart" word by looking at a hypothetical STATE
\ target word to detect if we are cross-compiling, but it would be a
\ host variable, so we would need `state [host] @ [target]` which is
\ pretty awkward as well.
:m s"
  [char] " parse
  swap
  postpone literal
  postpone literal
; immediate

:m s" [char] " parse ;

require ./core/conditionals.fs
require ./core/basic-loops.fs
require ./core/counted-loops.fs
require ./core/case.fs

( n1 n2 -- n )
: max
  over over >
  if drop else nip then ;

( n1 n2 -- n )
: min
  over over <
  if drop else nip then ;

\ Taken from the standard
: within  ( u1 u2 u3 -- flag )
  over - >r - r> u< ;

: count ( c-addr1 -- c-addr2 u )
  dup c@ swap char+ swap ;

include ./core/memory.fs

include ../shared/core.fs
