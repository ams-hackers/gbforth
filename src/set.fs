struct
  cell% field set-elt-value
  cell% field set-elt-next
end-struct set-elt%

: make-set ( -- set )
  cell allocate throw
  0 over ! ;

: add-to-set ( n set -- )
  set-elt% %alloc >r
  tuck
  @ r@ set-elt-next !
    r@ set-elt-value !
  r> swap ! ;

: in? ( n set -- flag )
  swap >r @
  begin ?dup while
    dup set-elt-value @ r@ = if
      drop rdrop true exit
    else
      set-elt-next @
    then
  repeat
  rdrop false ;

: free-set ( set -- )
  @
  begin ?dup while
    dup set-elt-next @
    swap free throw
  repeat ;

