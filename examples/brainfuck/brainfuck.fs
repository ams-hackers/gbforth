title: BRAINFUCK
makercode: TK

require ibm-font.fs
require term.fs

10   CONSTANT   rs-size
2000 CONSTANT tape-size

CREATE   rs   rs-size allot
CREATE tape tape-size allot

VALUE ip
VALUE rp
VALUE tp
VALUE start
VALUE length

: tape@ ( -- c ) tape tp + c@ ;
: tape! ( c -- ) tape tp + c! ;

: >ret ( c -- )
  rs rp + c!
  rp 1+ TO rp ;

: ret> ( -- c )
  rp 1- TO rp
  rs rp + c@ ;

: reset ( -- )
  0 TO ip
  0 TO rp
  0 TO tp
  rs rs-size erase
  tape tape-size erase ;

: skip-to-] ( -- )
  0
  BEGIN
    ip 1+ TO ip
    start ip + c@ CASE
      [char] [ OF 1+ ENDOF
      [char] ] OF dup 0= IF drop exit ELSE 1- THEN ENDOF
    ENDCASE
  AGAIN ;

: interpret ( c-addr u -- )
  TO length
  TO start
  BEGIN
    start ip + c@ CASE
      [char] + OF tape@ 1 + tape! ENDOF
      [char] - OF tape@ 1 - tape! ENDOF
      [char] > OF tp 1 + TO tp ENDOF
      [char] < OF tp 1 - 0 max TO tp ENDOF
      [char] [ OF tape@ IF ip >ret ELSE skip-to-] THEN ENDOF
      [char] ] OF ret> 1- TO ip ENDOF
      [char] . OF tape@ emit ENDOF
    ENDCASE
    ip 1+ TO ip
    ip length = IF exit THEN
  AGAIN ;

: run ( c-addr u -- )
  reset interpret ;

: main
  install-font
  init-term
  0 17 at-xy ." Running..."
  0 0 at-xy
  s" ++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++."
  run
  11 17 at-xy ." done" ;
