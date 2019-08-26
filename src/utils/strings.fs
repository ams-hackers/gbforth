( Persist a string from pad to stable memory )
: persist-string ( c-addr u -- c-addr' u' )
  dup >r here dup >r
  swap
  dup cells allot
  move
  r> r> ;

: append-to { c-addr u addr -- addr' }
  c-addr addr u move
  addr u + ;

: append-string ( c-addr1 u1 c-addr2 u2 -- c-addr3 u3 )
  2swap dup >r
  pad append-to

  over >r
  append-to

  drop pad r> r> + ;

( Remove the extension of a filename )
: sans-extname ( c-addr u -- c-addr' u' )
  dup 1+ 1 ?do
    2dup i - + c@
    [char] . = if i - unloop exit then
  loop ;

( Replace the extension of a filename )
: replace-ext ( c-addr u c-addr u -- c-addr' u' )
  2swap sans-extname 2swap append-string persist-string ;
