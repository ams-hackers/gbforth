ROM

CREATE aaa #33 ,
CREATE bbb #5566 , #44 c,

CREATE foo #10 cells allot
CREATE baz #1122 , aaa c@ c,
bbb cell+ c@ foo char+ c!
bbb @ foo #9 cells + !
#99 baz +!

RAM

: main
  baz @
  baz cell+ c@
  foo char+ c@
  foo #9 cells + @ ;
