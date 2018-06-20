
CREATE aaa #33 c, #44 c,
CREATE bbb #5566 ,

CREATE foo #10 cells allot
CREATE baz #1122 , aaa c@ c,
aaa 1+ c@ foo cell+ c!
bbb @ foo #9 cells + !

: main
  baz @
  baz cell+ c@
  foo cell+ c@
  foo #9 cells + @ ;
