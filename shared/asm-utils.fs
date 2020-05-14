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

: ps-dup-after-dec-c,
  H ->A-> [C] ld,
  C dec,
  L ->A-> [C] ld, ;

: ps-dup,
  C dec,
  ps-dup-after-dec-c, ;

( DE to the second element of the stack )
: ps-tuck-de,
  C dec,
  D ->A-> [C] ld,
  C dec,
  E ->A-> [C] ld, ;

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

: ps-over-de-nip-dec-c,
  [C] ->A-> E ld,
  C inc,
  [C] ->A-> D ld, ;

: ps-over-de-nip,
  ps-over-de-nip-dec-c,
  C inc, ;

: ps-over-de,
  ps-over-de-nip-dec-c,
  C dec, ;

: ps-push-de,
  ps-dup,
  DE->HL, ;

: ps-swap,
  ps-over-de-nip-dec-c,
  ps-dup-after-dec-c,
  DE->HL, ;

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
\   - DE is negated
\ Based on https://wikiti.brandonw.net/index.php?title=Z80_Routines:Math:Division#16.2F16_division
: HL+/DE+,
  negate-DE,

  H A ld,
  L C ld,
  0 # HL ld,
  16 # B ld,
  \ During the loop, the high B+16 bits of HLAC form what starts as the divisor
  \ and becomes the remainder. The low 16-B bits of AC are the quotient so far.
  begin,
    \ Shift in another bit
    C sla,
    rla,
    L rl,
    H rl,

    \ Try to subtract the divisor
    HL push,
    DE HL add,
    \ This condition is written #NC first to make the cycle count less varied.
    \ Coincidentally, this makes the division constant-time.
    #NC if,
      HL pop,
    else,
      \ Subtracted successfully
      2 # SP add,
      C inc,
    then,

    B dec,
  #Z until,
  A B ld, ;

: HL/DE+,
  H 7 # bit, #nz if,
    ps-negate,
    HL+/DE+,
    negate-BC,
    \ remainder <> 0
    H|L->A, #nz if,
      BC dec,
      \ HL = dividend - HL[remainder] = -(HL + -dividend) = -(HL + DE)
      DE HL add,
      ps-negate,
    then,
  else,
    HL+/DE+,
  then, ;

\ Divide _signed_ HL by DE.
\   - Quotient is put into BC
\   - Remainder into HL
: HL/DE,
  D 7 # bit, AF push, #nz if,
    negate-DE,
  then,
  HL/DE+,    \ BC = quotient, HL = remainder
  AF pop, #nz if,
    negate-BC,
  then,
;

[endasm]
