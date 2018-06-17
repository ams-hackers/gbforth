: my-max
  2dup < if nip else drop then ;

: main
   0 1 my-max
   2 1 my-max
   3 3 my-max ;
