require ./utils.fs

32 kB constant sym-size
create sym-base sym-size allot
variable sym-offset-variable

: sym-offset sym-offset-variable @ ;
: sym-offset! sym-offset-variable ! ;
: sym-offset+! sym-offset-variable +! ;

: sym sym-base sym-size ;

: sym-offset>addr
  sym-base + ;

: sym@ ( offset -- val )
  sym-offset>addr c@ ;

: sym! ( val offset -- )
  sym-offset>addr c! ;

: sym,
  sym-offset sym!
  $1 sym-offset+! ;

: sym-sp, $20 sym, ;
: sym-nl, $0A sym, ;

: sym-move ( addr u -- )
  dup >r
  sym-offset sym-offset>addr swap move
  r> sym-offset+! ;

: sym"
  [char] " parse sym-move ;

0 Value sym-fd
: dump-sym ( c-addr u -- )
  w/o create-file throw TO sym-fd
  sym sym-fd write-file throw
  sym-fd close-file throw ;

: addr-to-str ( nn -- )
  base @ >r      \ store original base
  hex            \ switch to hex
  0              \ convert to unsigned double
  <<#            \ start conversion
  # # # #        \ convert four least-significant digits
  #>             \ complete conversion
  #>>            \ release hold area
  r> BASE ! ;    \ reset original base

: add-to-sym, ( c-addr u nn -- )
  s" 00:" sym-move
  addr-to-str sym-move
  sym-sp,
  sym-move
  sym-nl, ;
