( A SUBROUTINE-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs

also gb-assembler


( TEMPORARY HACK: Don't break the game! )
$4400 ==>

\ : PS $FFFD -> $FF6F ;  \ Parameter Stack address
\ : PSP C ;     \ Parameter Stack Pointer
\ : (PSP) [C] ; \ TOS content

: ps-clear,
  $EF # C ld, ; ( init PSP to 0 )

( 8c/6b )
: ps-push-hl,
  C dec,        ( 1c/1b )
  H A ld,       ( 1c/1b )
  A [C] ld,     ( 2c/1b )
  C dec,        ( 1c/1b )
  L A ld,       ( 1c/1b )
  A [C] ld, ;   ( 2c/1b )

( 11c/9b )
: ps-push-lit,
  ps-push-hl,   ( 8c/6b )
  ( nn ) # HL ld, ( 3c/3b )
;

( 8c/6b )
: ps-pop-hl,
  [C] A ld,     ( 2c/1b )
  A L ld,       ( 1c/1b )
  C inc,        ( 1c/1b )
  [C] A ld,     ( 2c/1b )
  A H ld,       ( 1c/1b )
  C inc, ;      ( 1c/1b )

( 8c/6b )
: ps-pop-de,
  [C] A ld,     ( 2c/1b )
  A E ld,       ( 1c/1b )
  C inc,        ( 1c/1b )
  [C] A ld,     ( 2c/1b )
  A D ld,       ( 1c/1b )
  C inc, ;      ( 1c/1b )

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
: ps-pop-hl, ps-pop-hl, ;

( 26c/19b )
code dup
ps-push-hl,   ( 8c/6b )
ret,          ( 4c/1b )

( 30c/20b )
code +
ps-pop-de,    ( 8c/6b )
DE HL add,    ( 2c/1b )
ret,          ( 4c/1b )

( 16c/7b -> 72c/46b )
code double
dup # call,   ( 6c/3b )
+ # call,     ( 6c/3b )
ret,          ( 4c/1b )

( 16c/7b -> 160c/99b )
code quadruple
double # call,  ( 6c/3b )
double # call,  ( 6c/3b )
ret,            ( 4c/1b )

[end-user-definitions]

previous
