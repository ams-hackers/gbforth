require term.fs
require assert.fs

0 assert-level!
: test-0
  $03 assert3( bye )            \ disabled
  $02 assert2( bye )            \ disabled
  $01 assert1( dup $01 <> )     \ disabled
  $00 assert0( 1 2 + 3 = ) ;    \ enabled - pass

1 assert-level!
: test-1
  $13 assert3( bye  )           \ disabled
  $12 assert2( 5 dup * 25 <> )  \ disabled
  $11 assert1( dup $11 = )      \ enabled - pass
  $10 assert0( 1 2 + 3 = ) ;    \ enabled - pass

2 assert-level!
: test-2
  $23 assert3( bye  )           \ disabled
  $22 assert2( 5 dup * 25 = )   \ enabled - pass
  $21 assert1( dup $21 = )      \ enabled - pass
  $20 assert0( 1 2 + 3 = ) ;    \ enabled - pass

3 assert-level!
: test-3
  $33 assert3( false )          \ enabled - should fail
  $32 assert2( true )
  $31 assert1( true )
  $30 assert0( true ) ;

: main
  test-0 test-1 test-2 test-3 ;
