defer answer

: answer-one 41 ;
: answer-two 42 ;
: answer-three 43 ;

: main
  ['] answer-three is answer
  answer
  ['] answer-two is answer
  ['] answer-one is answer
  answer
;
