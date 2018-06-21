rom variable foo
ram variable bar

$1337 foo !

: main
  $4242 bar !
  foo @
  bar @ ;
