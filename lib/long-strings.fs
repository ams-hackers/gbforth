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

\ define an additional word wrap. to display
\ long text wrapped within the screen. returns
\ the remaining addr' u' portion if the text
\ does not fit on one page, or 0 0 if all text
\ was written to the screen

: wrap. ( addr u -- addr' u' )
  begin
    dup SCRN_X_B >
  while
    over SCRN_X_B
    begin 1- 2dup + c@ bl = until
    dup 1+ >r
    begin 1- 2dup + c@ bl <> until
    1+ type cr
    r> /string
    cursor-y @ SCRN_Y_B = if exit then
  repeat type cr 0 0 ;
