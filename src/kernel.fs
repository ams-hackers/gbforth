( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs

also gb-assembler


( TEMPORARY HACK: Don't break the game! )
$4400 ==>

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

: ps-over-de,
  [C] A ld, A E ld,
  C inc,
  [C] A ld, A D ld,
  C inc, ;

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

: code rom-offset constant ;

( Code Words

  Those are primitive [code] words written in dmg-forth.
)

[user-definitions]

: ps-clear, ps-clear, ;
: ps-push-lit, ps-push-lit, ;

( x -- x x )
code dup
ps-dup,
ret,

( a b -- c )
code +
ps-over-de,
DE HL add,
ret,

( c-addr -- x )
code c@
[HL] E ld,
$0 # H ld,
E L ld,
ret,

( x c-addr -- )
code c!
ps-over-de,
E [HL] ld,
ps-drop,
ret,

\ : double dup + ;
code double
dup # call,
+ # call,
ret,

\ : quadruple double double ;
code quadruple
double # call,
double # call,
ret,

[end-user-definitions]

previous
