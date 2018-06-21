(
  ***** Constants *****
)

#32 constant bl

$0000 constant false
$FFFF constant true

#2 constant cell


(
  ***** Stack Manipulation *****
)

[host]
include ../src/asm-utils.fs
[target]


( x -- x x )
code dup
ps-dup,
end-code

( x -- )
code drop
ps-drop,
end-code

( a b -- b a )
code swap
ps-swap,
end-code

( a b -- b )
code nip
C inc,
C inc,
end-code

( a b -- a b a )
code over
ps-over,
end-code

( a b -- b a b )
code tuck
ps-swap,
ps-over,
end-code

(
  ***** Arithmetic *****
)

( a b -- c )
code +
ps-over-de-nip,
DE HL add,
end-code

( a b -- c )
code -
ps-pop-de,
L A ld,
E A sub,
A L ld,
H A ld,
D A sbc,
A H ld,
end-code

code * ( a b -- c )
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

( a b -- c )
code /
ps-pop-de,  \ dividend to HL, divisor to DE
BC push,    \ store SP
#0 # BC ld, \ quotient = 0
begin, \ repeated substraction HL - DE
  L A ld, E A sub, A L ld, \ L - E
  H A ld, D A sbc, A H ld, \ H - D - carry
#NC while, \ remainder <0 ? done!
  BC inc,      \ inc quotient
repeat,    \ repeat substraction
  B H ld, C L ld, \ move BC [quotient] to HL [TOS]
  BC pop,         \ restore SP
end-code

( a b -- c )
code mod
ps-pop-de,  \ dividend to HL, modulus to DE
begin, \ repeated substraction HL - DE
  L A ld, E A sub, A L ld, \ L - E
  H A ld, D A sbc, A H ld, \ H - D - carry
#C until, \ remainder <0 ? done!
  DE HL add,
end-code

( x -- x )
code 1+
\ avoid using inc, because of OAM bug
$1 # DE ld,
DE HL add,
end-code

( x -- x )
code 1-
\ avoid uising dec, because of OAM bug
L A ld, $1 # A sub, A L ld,
H A ld, $0 # A sbc, A H ld,
end-code

( a b -- c )
code max
ps-over-de-nip,
  \ compare higher bytes
  H A ld, D A cp, \ H D cp,
there> #C jp, \ H<D
#NZ ret, \ H>D
  \ higher bytes equal, compare lower
  L A ld, E A cp, \ L E cp,
#NC ret, \ L>=E
>here
  D H ld, E L ld,
end-code

( a b -- c )
code min
ps-over-de-nip,
  \ compare higher bytes
  H A ld, D A cp, \ H D cp,
#C ret, \ H<D
there> #NZ jp, \ H>D
  \ higher bytes equal, compare lower
  L A ld, E A cp, \ L E cp,
#C ret, \ L<E
>here
  D H ld, E L ld,
end-code

(
  ***** Bitwise Operations *****
)

( a b -- c )
code and
ps-over-de-nip,
H A ld, D A and, A H ld,
L A ld, E A and, A L ld,
end-code

( a b -- c )
code or
ps-over-de-nip,
H A ld, D A or, A H ld,
L A ld, E A or, A L ld,
end-code

( a b -- c )
code xor
ps-over-de-nip,
H A ld, D A xor, A H ld,
L A ld, E A xor, A L ld,
end-code

( a n -- b )
code lshift
ps-pop-de,
begin,
  E A ld, $1 # A sub, A E ld,
  D A ld, $0 # A sbc, A D ld,
#C ret,
  HL HL add,
repeat,
-end-code

( a n -- b )
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

( x -- x )
code 2*
HL HL add,
end-code

( x -- x )
code 2/
H srl,
L rr,
end-code

(
  ***** Numeric Comparison *****
)

( x y -- f )
code <
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

( x y -- f )
code >
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

( x y -- f )
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
  H ->A-> cpl, A H ld,
  L ->A-> cpl, A L ld,
end-code


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

( c-addr -- x )
code @
[HL+] A ld,
[HL] H ld,
A L ld,
end-code

( x c-addr -- )
code !
ps-over-de-nip,
E A ld, A [HL+] ld,
D [HL] ld,
ps-drop,
end-code

( n c-addr -- )
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

( -- addr )

$C000 constant DP
: here DP @ ;
: allot ( n -- )
  DP +! ;

: unused ( -- n )
  $CFFF here - ; \ end of RAM bank 0

: char+ ( x -- x )
  1+ ;

: chars ( x -- x )
  ( 1 * ) ;

: cell+ ( x -- x )
  cell + ;

: cells ( x -- x )
  cell * ;

: aligned ( x -- x )
  dup #2 mod if
    1+
  then ;

: align ( -- )
  here #2 mod if
    #1 allot
  then ;

: , ( x -- )
  here !
  cell allot ;

: c, ( x -- )
  here c!
  #1 allot ;

code execute
\ The stack contains the return address for execute. We put HL on top,
\ so RET will take it and jump there.
hl push,
( but just before that, we have to drop the address from the data stack )
ps-drop,
end-code


code cmove ( c-from c-to u -- )
( DE = c-to )
( BC = c-from )
( HL = u )

( DE = c-to )
[C] ->A-> E ld,  C inc,
[C] ->A-> D ld,  C inc,

( BC = c-from )
\ Taking this argument is quite more tricky, as C is for the stack.
\ Postpone overriding C until the very end by retrieving B first.
C inc, BC push,
[C] ->A-> B ld,  C dec,
[C] ->A-> C ld,

begin, H|L->A, #NZ while,
  ( copy one byte )
  [BC] ->A-> [DE] ld,
  BC inc,
  DE inc,

  HL dec,
repeat,

BC pop, C inc,
ps-drop,
end-code

code cmove> ( c-from c-to u -- )
( DE = c-to )
( BC = c-from )
( HL = u )

( DE = c-to )
[C] ->A-> E ld,  C inc,
[C] ->A-> D ld,  C inc,

( BC = c-from )
\ Taking this argument is quite more tricky, as C is for the stack.
\ Postpone overriding C until the very end by retrieving B first.
C inc, BC push,
[C] ->A-> B ld,  C dec,
[C] ->A-> C ld,

( c-to += u-1 )
HL push,
DE HL add,
H D ld, L E ld,
DE dec,

( c-from += u-1 )
HL pop, HL push,
BC HL add,
H B ld, L C ld,
BC dec,
HL pop,

begin, H|L->A, #NZ while,
  ( copy one byte )
  [BC] ->A-> [DE] ld,
  BC dec,
  DE dec,

  HL dec,
repeat,

BC pop, C inc,
ps-drop,
end-code

require lcd.fs


code fill ( c-addr u c -- )
( DE = u )
( BC = c-addr )
( HL = c )

( DE = u )
[C] ->A-> E ld,  C inc,
[C] ->A-> D ld,  C inc,

( BC = c-addr )
\ Taking this argument is quite more tricky, as C is for the stack.
\ Postpone overriding C until the very end by retrieving B first.
C inc, BC push,
[C] ->A-> B ld,  C dec,
[C] ->A-> C ld,

( HL = c )
begin, D|E->A, #NZ while,
  lcd_WaitVRAM
  L ->A-> [BC] ld,
  BC inc,
  DE dec,
repeat,

BC pop, C inc,
ps-drop,
end-code

( Tools )

code bye ( -- )
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

: rot ( a b c -- b c a )
  >r swap r> swap ;

: -rot ( a b c -- c a b )
  swap >r swap r> ;

: 2dup ( a b -- a b a b )
  over over ;

: 2drop ( x x -- )
  drop drop ;

: 2nip ( a b c d -- c d )
  >r >r drop drop r> r> ;

: 2over ( a b c d -- a b c d a b )
  >r >r over over r> -rot r> -rot ;

: 2swap ( a b c d -- c d a b )
  >r -rot r> -rot ;

: 2rot ( a b c d e f -- c d e f a b )
  >r >r >r -rot r> -rot r> -rot r> -rot ;

: 2tuck ( a b c d -- c d a b c d )
  >r dup >r -rot r> r> swap >r dup >r -rot r> r> swap ;

: 0= 0 = ;

:m endif postpone then ; immediate

( CASE...ENDCASE implementation )

:m case 0 ; immediate

:m of
  1+ >r
  postpone over
  postpone =
  postpone if
  postpone drop
  r>
; immediate

:m endof
  >r postpone else r>
; immediate

:m endcase
  postpone drop
  0 ?do postpone then loop
; immediate
