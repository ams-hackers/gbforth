( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs
require ./utils/bytes.fs

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

( Helper words for stack manipulation )

$FFFE constant SP0 \ end of HRAM
$CFFF constant RS0 \ end of RAM bank 0
$C000 constant DP0 \ start of RAM bank 0

variable DP
DP0 2 + DP ! \ init dictionary pointer

: dp-init,
  DP @ dup
  higher-byte # A ld, A DP0 ]* ld,
  lower-byte # A ld, A DP0 1 + ]* ld, ;

: ps-clear,
  SP0 $FF00 - # C ld, ;

: ps-init,
  ps-clear,
  RS0 # SP ld, ;

: ps-push-lit,
  C dec,
  H A ld, A [C] ld,
  C dec,
  L A ld, A [C] ld,
  # HL ld, ;

( Words used by the cross compiler )

: xliteral, ps-push-lit, ;
: xcompile, # call, ;
: xreturn, ret, ;

[endasm]
