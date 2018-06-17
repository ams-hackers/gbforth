: my-max
  2dup < if nip else drop then ;

: my-?dup
  dup dup 0 = if drop then ;


: main
   0 1 my-max
   2 1 my-max
   3 3 my-max

   0 my-?dup
   5 my-?dup ;
