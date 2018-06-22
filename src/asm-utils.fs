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

[endasm]
