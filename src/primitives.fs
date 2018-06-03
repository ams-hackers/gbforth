require ./asm.fs
require ./cross.fs

[asm]

( TEMPORARY HACK: Don't break the game! )
$4400 ==>

: code
  parse-name
  2dup rom-offset sym
  nextname xcreate ;

(
  ***** Stack Manipulation *****
)

( x -- x x )
code dup
ps-dup,
ret,

( x -- )
code drop
ps-drop,
ret,

( a b -- b a )
code swap
ps-swap,
ret,

( a b -- b )
code nip
C inc,
C inc,
ret,

( a b -- a b a )
code over
ps-over,
ret,

( a b -- b a b )
code tuck
ps-swap,
ps-over,
ret,

( a b c -- b c a )
code rot
ps-pop-de,
ps-swap,
ps-push-de,
ps-swap,
ret,

( a b c -- c a b )
code -rot
ps-swap,
ps-pop-de,
ps-swap,
ps-push-de,
ret,

(
  ***** Arithmetic *****
)

( a b -- c )
code +
ps-over-de-nip,
DE HL add,
ret,

( a b -- c )
code -
ps-pop-de,
L A ld,
E A sub,
A L ld,
H A ld,
D A sbc,
A H ld,
ret,

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
ret,

( a b -- c)
code /
ps-pop-de,  \ dividend to DE, divisor to HL
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
ret,

( x -- x )
code 1+
\ avoid using inc, because of OAM bug
$1 # DE ld,
DE HL add,
ret,

( x -- x )
code 1-
\ avoid uising dec, because of OAM bug
L A ld, $1 # A sub, A L ld,
H A ld, $0 # A sbc, A H ld,
ret,

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
ret,

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
ret,

(
  ***** Bitwise Operations *****
)

( a b -- c )
code and
ps-over-de-nip,
H A ld, D A and, A H ld,
L A ld, E A and, A L ld,
ret,

( a b -- c )
code or
ps-over-de-nip,
H A ld, D A or, A H ld,
L A ld, E A or, A L ld,
ret,

( a b -- c )
code xor
ps-over-de-nip,
H A ld, D A xor, A H ld,
L A ld, E A xor, A L ld,
ret,

( a n -- b )
code lshift
ps-pop-de,
begin,
  E A ld, $1 # A sub, A E ld,
  D A ld, $0 # A sbc, A D ld,
#C ret,
  HL HL add,
repeat,

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

( x -- x )
code 2*
HL HL add,
ret,

( x -- x )
code 2/
H srl,
L rr,
ret,

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
ret,

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
ret,

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
ret,

(
  ***** Memory Access *****
)

( c-addr -- c )
code c@
[HL] L ld,
$0 # H ld,
ret,

( c c-addr -- )
code c!
ps-over-de-nip,
E [HL] ld,
ps-drop,
ret,

( c-addr -- x )
code @
[HL+] A ld,
[HL] L ld,
A H ld,
ret,

( x c-addr -- )
code !
ps-over-ae-nip,
A [HL+] ld,
E [HL] ld,
ps-drop,
ret,

code execute
\ The stack contains the return address for execute. We put HL on top,
\ so RET will take it and jump there.
hl push,
( but just before that, we have to drop the address from the data stack )
ps-drop,
ret,


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
ret,



[endasm]
