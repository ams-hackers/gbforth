require formatted-output.fs

create price-1   10 chars allot
create price-2   10 chars allot
create price-3   10 chars allot
create account-1 10 cells allot
create account-2 10 cells allot
create account-3 10 cells allot
create number-1  10 cells allot
create number-2  10 cells allot
create number-3  10 cells allot

: format-price ( n -- addr u )
  <#
  # #
  [char] . hold
  #s
  [char] ¥ hold
  #> ;

: format-account ( n -- addr u )
  dup abs
  <#
  over 0< if [char] ) hold then
  #s
  swap 0< if [char] ( hold then \ )
  #> ;

: format-number ( n -- addr u )
  dup abs
  <# #s swap sign #> ;

: main
       4 format-price     price-1 place
      21 format-price     price-2 place
    1989 format-price     price-3 place
   21500 format-account account-1 place
      -0 format-account account-2 place
  -21500 format-account account-3 place
    9876 format-number   number-1 place
       0 format-number   number-2 place
   -1234 format-number   number-3 place
   price-1 price-2 price-3
   account-1 account-2 account-3
   number-1 number-2 number-3 ;
