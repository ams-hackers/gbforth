[host]
variable assert-level
1 assert-level !

: assertn
  assert-level @ > if
    postpone ( \ )
  then ;

:m assert-level! assert-level ! ;
[target]

:m assert0( 0 assertn ; immediate
:m assert1( 1 assertn ; immediate
:m assert2( 2 assertn ; immediate
:m assert3( 3 assertn ; immediate
:m assert( POSTPONE assert1( ; immediate

[host]
: sourcepos ( -- c-addr u )
  sourcefilename pad place
  s" :" pad +place
  sourceline# s>d <# #s #> pad +place
  pad count ;
[target]

: (assert) ( f c-addr u -- )
  rot 0= if
    page ." failed assertion"
    cr ( sourcepos ) type bye
  then 2drop ;

:m ) ( f -- )
  sourcepos sliteral
  postpone (assert) ; immediate
