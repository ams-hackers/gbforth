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


: move ( c-from c-to u -- )
  >r 2dup u< if
    r> cmove>
  else
    r> cmove
  then ;




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



