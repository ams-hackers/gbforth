cell negate constant -cell

:m STACK ( u -- )
  CREATE 1+ cells allot ;

: push ( x a-addr -- )
  tuck dup @ cell+ + ! cell swap +! ;

: pop ( a-addr -- x )
  dup dup @ + @ swap -cell swap +! ;

: peek ( a-addr -- x )
  dup @ + @ ;

: empty? ( a-addr -- f )
  @ 0= ;

: size ( a-addr -- u )
  @ cell / ;

: clear ( a-addr -- )
  0 swap ! ;
