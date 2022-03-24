: foo ;

' foo constant foo-xt-top

:m tick: ' ;

tick: foo constant foo-xt-meta

: main
  ['] foo
  foo-xt-top
  foo-xt-meta ;
