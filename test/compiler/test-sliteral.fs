: mystring [ s" test" ] sliteral ;

: main
  mystring drop     c@
  mystring drop 1 + c@
  mystring drop 2 + c@
  mystring drop 3 + c@ ;
