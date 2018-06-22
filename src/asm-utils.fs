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

: ps-invert,
  H ->A-> cpl, A H ld,
  L ->A-> cpl, A L ld, ;

: ps-negate,
  ps-invert,
  HL inc, ;

: negate-DE,
  D ->A-> cpl, A D ld,
  E ->A-> cpl, A E ld,
  DE inc, ;

: negate-BC,
  B ->A-> cpl, A B ld,
  C ->A-> cpl, A C ld,
  BC inc, ;


\ Divide unsigned HL by DE.
\   - Quotient is put into BC
\   - Remainder into HL 
: HL+/DE+,
  #0 # BC ld,
  begin, \ HL -= DE
    L A ld, E A sub, A L ld,
    H A ld, D A sbc, A H ld,
  #NC while, \ remainder <0 ? done!
    BC inc,
  repeat,
  DE HL add, ;

: HL/DE+,
  H 7 # bit, #nz if,
    ps-negate,
    HL push,
    HL+/DE+,
    \ reminder = 0
    H|L->A, #z if,
      negate-BC,
      DE pop,
    else,
      negate-BC,
      BC dec,
      DE pop,      \ divident
       \ DE = DE[divident] - HL[reminder]
      E A ld, L A sub, A E ld,
      D A ld, D A sbc, A D ld,
      \ DE -> HL
      D H ld,
      E L ld,
    then,
  else,
    HL+/DE+,
  then, ;

\ Divide _signed_ HL by DE.
\   - Quotient is put into BC
\   - Remainder into HL
: HL/DE,
  D 7 # bit, #nz if,
    negate-DE,
    HL/DE+,    \ BC = quotient, HL = remainder
    negate-BC,
  else,
    HL/DE+,
  then,
;


[endasm]
