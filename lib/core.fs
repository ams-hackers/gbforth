(
  ***** Constants *****
)

#32 constant bl


(
  ***** Stack Manipulation *****
)

[host]
[asm]

: ->A-> A ld, A ;

: true->HL,
  $FFFF # HL ld, ;

: false->HL,
  $0000 # HL ld, ;

: HL->DE,
  H D ld,
  L E ld, ;

: DE->HL,
  D H ld,
  E L ld, ;

( Adjust flags #NZ and #Z if HL is zero )
: H|L->A,
  H A ld, L A or, ;

( Adjust flags #NZ and #Z if DE is zero )
: D|E->A,
  D A ld, E A or, ;

: ps-dup,
  C dec,
  H ->A-> [C] ld,
  C dec,
  L ->A-> [C] ld, ;

: ps-drop,
  [C] ->A-> L ld,
  C inc,
  [C] ->A-> H ld,
  C inc, ;

: ps-pop-de,
  HL->DE,
  ps-drop, ;

: ps-over-ae-nip,
  [C] ->A-> E ld,
  C inc,
  [C] A ld,
  C inc, ;

: ps-over-de-nip,
  [C] ->A-> E ld,
  C inc,
  [C] ->A-> D ld,
  C inc, ;

: ps-over-de,
  [C] ->A-> E ld,
  C inc,
  [C] ->A-> D ld,
  C dec, ;

: ps-push-de,
  ps-dup,
  DE->HL, ;

: ps-swap,
  ps-over-de-nip,
  ps-push-de, ;

: ps-over,
  ps-over-de,
  ps-push-de, ;

[endasm]
[endhost]


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

( a b c -- b c a )
code rot
ps-pop-de,
ps-swap,
ps-push-de,
ps-swap,
end-code

( a b c -- c a b )
code -rot
ps-swap,
ps-pop-de,
ps-swap,
ps-push-de,
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
code here
ps-dup,
$C001 ]* A ld, A H ld,
$C000 ]* A ld, A L ld,
end-code

( n -- )
code allot
$C001 ]* A ld, A D ld,
$C000 ]* A ld, A E ld,
DE HL add,
H A ld, A $C001 ]* ld,
L A ld, A $C000 ]* ld,
ps-drop,
end-code

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


require video.fs


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

: 2dup over over ;
