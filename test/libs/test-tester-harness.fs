require tester.fs

: main
  init-tester
  888
  T{ 1 2 3 rot     -> 2 3 1   }T
  T{ true -> false }T
  T{ 4 dup *       -> 16      }T
  T{ 1 2 -> 1 }T
  T{ 5 6 tuck over -> 6 5 6 5 }T
  #errors @ ;
