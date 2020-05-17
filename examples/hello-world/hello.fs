title: EXAMPLE

require ibm-font.fs
require term.fs
require interrupt.fs

variable num
: num? 3 8 at-xy num c@ . space ;
: num++ num c@ 1+ num c! ;

p10-interrupt: num++

: main
  install-font
  init-term
  3 7 at-xy
  ." Hello World !"
  0 num !
  enable-key-interrupts
  begin num? again ;
