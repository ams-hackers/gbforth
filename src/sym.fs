require ./utils.fs

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

: to-symbol-file ( c-addr u -- c-addr' u' )
  sans-extname s" .sym" append-string ;

output-file to-symbol-file w/o create-file throw Value sym-out

: sym-cr
  #10 sym-out emit-file throw ;

: sym-write ( c-addr u -- )
  sym-out write-file throw
  sym-out flush-file throw  ;

: addr-to-str ( nn -- c-addr u )
  base @ >r
  hex
  0
  <<#
  # # # #
  #>
  #>>
  r> BASE ! ;

: sym-emit ( c-addr u nn -- )
  s" 00:"       sym-write
  addr-to-str   sym-write
  s"  "         sym-write
  ( c-addr u )  sym-write
  sym-cr ;
