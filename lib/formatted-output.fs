\ Numeric ouptut routines

\ WARNING: This is just an approximation to the ANS words. It works
\ only on base 10 and single-cell numbers for now.
\

create holdbuf 10 chars allot
create holdbuf-end

variable holdptr

: <#
  holdbuf-end holdptr ! ;

: hold ( c -- )
  -1 holdptr +!
  holdptr @ c! ;

: digit>ascii ( n -- )
  [char] 0 + ;

: # ( n1 -- n2 )
  10 /mod swap digit>ascii hold ;

: #s ( n -- 0 )
  begin # dup 0= until ;

: sign ( n -- )
  0< if [char] - hold then ;

: #> ( n -- addr u )
  drop holdptr @ holdbuf-end over - ;
