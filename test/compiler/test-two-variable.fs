2variable foo

rom 2variable bar
$1337 bar !
$42 bar cell+ !

: main
  $9292 foo       !
  $5252 foo cell+ !
  bar @ bar cell+ @
  foo @ foo cell+ @ ;
