( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs

[asm]

( Assume you have the following code

  : double dup + ;
  : quadruple double double ;

  This kernel uses subroutine-threading Forth. The picture below
  illustrates how the compiled words would look like:

              +------
              |  ...                            DUP [asm]
              +------
                ^
                |
               /
       +----------+--------+------+
       | CALL dup | CALL + | RET  |             DOUBLE
       +----------+--------+------+
         ^
         \____________________
                              \
       +-------------+-------------+-----+
       | CALL double | CALL double | RET |      QUADRUPLE
       +-------------+-------------+-----+

  For all colon definitions, the code field simply contains CALLs to
  every word [or primitive] address that is part of the word definition.
)

( Helper words for moving between registers )

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


( Helper words for stack manipulation )

$FFFE constant SP0 \ end of HRAM
$CFFF constant RS0 \ end of RAM bank 0

: ps-clear,
  SP0 $FF00 - # C ld, ;

: ps-init,
  ps-clear,
  RS0 # SP ld, ;

: ps-dup,
  C dec,
  H ->A-> [C] ld,
  C dec,
  L ->A-> [C] ld, ;

: ps-push-lit,
  ps-dup,
  # HL ld, ;

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

( Words used by the cross compiler )

: xliteral, ps-push-lit, ;
: xcompile, # call, ;
: xreturn, ret, ;

[endasm]
