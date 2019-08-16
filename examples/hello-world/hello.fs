title: EXAMPLE

require ibm-font.fs
require term.fs

: main
  install-font
  init-term
  3 7 at-xy
  ." Hello World !" ;
