title: EXAMPLE
gamecode: HELO
makercode: RX

require gbhw.fs
require input.fs
require term.fs

s" Hello World !"
constant TitleLength
constant TitleOffset

: copy-title
  TitleOffset TitleLength type ;

( program start )

: rSCX+! rSCX c@ + rSCX c! ;
: rSCY+! rSCY c@ + rSCY c! ;

: handle-input
  begin
    rDIV c@ [ $FF 8 / ]L < if
      key-state
      dup k-right and if -5 rSCX+! then
      dup k-left  and if  5 rSCX+! then
      dup k-up    and if  5 rSCY+! then
      dup k-down  and if -5 rSCY+! then
      \ If there no key pressed, wait for one
      0= if halt then
    then
  again ;

: main
  init-term
  init-input
  page
  copy-title
  handle-input ;
