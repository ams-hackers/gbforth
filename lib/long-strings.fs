require ./formatted-output.fs

\ override place, count and +place
\ with a cell-counted version that
\ allows for 255+ character strings

: place ( c-addr1 u1 c-addr2 -- )
  over >r rot over cell+ r> move ! ;

: count ( c-addr1 -- c-addr2 u )
  dup cell+ swap @ ;

: +place ( c-addr1 u1 c-addr2 -- )
  over over >r >r
  count chars +
  swap chars move
  r> r@ @ + r> ! ;

\ override additional words from strings lib
\ to work with redefined place and +place

: c+place ( c c-addr )
  dup @ char+ over !
  count 1- chars + ! ;

: #+place ( u c-addr -- )
  swap
  dup abs <# #s swap sign #>
  rot +place ;
