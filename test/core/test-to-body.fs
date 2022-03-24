RAM CREATE foo

ROM
:m val: CREATE , DOES> @ ;
42 val: bar

: main
   ['] foo >BODY foo =
   ['] bar >BODY @ 42 =
   bar 42 = ;
