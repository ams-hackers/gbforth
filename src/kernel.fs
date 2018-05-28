( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs

also gb-assembler

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

: ps-clear,
  $EF # C ld, ;

: ps-dup,
  C dec,
  H A ld, A [C] ld,
  C dec,
  L A ld, A [C] ld, ;

: ps-push-lit,
  ps-dup,
  # HL ld, ;

: ps-drop,
  [C] A ld, A L ld,
  C inc,
  [C] A ld, A H ld,
  C inc, ;

: ps-over-de-nip,
  [C] A ld, A E ld,
  C inc,
  [C] A ld, A D ld,
  C inc, ;

: ps-over-de,
  [C] A ld, A E ld,
  C inc,
  [C] A ld, A D ld,
  C dec, ;

: ps-push-de,
  ps-dup,
  D H ld,
  E L ld, ;

: xliteral, ps-push-lit, ;
: xcompile, # call, ;
: xreturn, ret, ;

previous
