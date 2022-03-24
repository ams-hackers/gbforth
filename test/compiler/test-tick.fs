: foo ;

' foo constant foo-xt-top

:m tick: ' ; immediate

tick: foo constant foo-xt-meta

: main
  ['] foo
  [ ' foo ]L
  foo-xt-top
  foo-xt-meta
  tick: foo ;
