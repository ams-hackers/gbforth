require ./asm.fs
require ./cross.fs

[asm]

( TEMPORARY HACK: Don't break the game! )
$4400 ==>

: code
  parse-name
  2dup rom-offset sym
  nextname xcreate ;

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

( a b -- c )
code +
ps-over-de-nip,
DE HL add,
ret,

code * ( a b -- c )
ps-over-de-nip,
BC push,
H B ld,
L C ld,
local
presume NoMul
  $0 # HL ld,
  $10 # A ld,
label MulLoop
  HL HL add,
  E rl,
  D rl,
NoMul #NC jp,
  BC HL add,
NoMul #NC jp,
  DE inc,
label NoMul
  A dec,
MulLoop #NZ jp,
end-local
BC pop,
\ ps-push-de, ( DE contains higher 2 bytes of result )
ret,

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
local
presume .end
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


( check if HL=0 )
H|L->A,
.end #Z jr,

label .body

( copy one byte )
[BC] ->A-> [DE] ld,
BC inc,
DE inc,

HL dec,

H|L->A,
.body #NZ jr,

label .end

BC pop, C inc,
ps-drop,
ret,

end-local





[endasm]
