rom

:m my-constant create , does> @ ;
:m magic-constant does> drop $5432 ;

$123 my-constant foo

$123 my-constant bar
magic-constant

ram

: main
  foo
  bar ;
