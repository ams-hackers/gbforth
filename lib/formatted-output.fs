\ Numeric ouptut routines

\ WARNING: This is just an approximation to the ANS words. It works
\ only on base 10 and single-cell numbers for now.
\

10 chars constant pad-size

create pad-base
pad-size allot
create pad-end

variable pad-pointer

: pad ( -- c-addr u )
  pad-end pad-pointer @ tuck - ;

: <#
  pad-end pad-pointer ! ;

: hold ( c -- )
  -1 pad-pointer +!
  pad-pointer @ c! ;

: digit>ascii ( n -- )
  [char] 0 + ;

: # ( n1 -- n2 )
  10 /mod swap digit>ascii hold ;

: #s ( n -- 0 )
  begin # dup 0= until ;

: sign ( n -- )
  0< if [char] - hold then ;

: #> ( n -- )
  drop pad ;
