require tester.fs

: main
  1
  T{ 1 2 3 rot -> 2 3 1 }T
  2
  T{ 4 dup * -> 16 }T
  3
  T{ true -> false }T \ should exit here
  4 ;
