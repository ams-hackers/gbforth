require ./formatted-output.fs

: toupper ( c -- c )
  dup
  [ char a ]L [ char z 1+ ]L within
  if 32 - then ;

: c+place ( c c-addr -- )
  dup c@ char+ over c!
  count 1- chars + c! ;

: #+place ( u c-addr -- )
  swap
  dup abs <# #s swap sign #>
  rot +place ;
