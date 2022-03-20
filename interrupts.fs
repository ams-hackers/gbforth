require lib/gbhw.fs
require lib/music.fs

require lib/ibm-font.fs
require lib/term.fs

\ variable marker
variable tac-count
: bleep-slower
  tac-count @ [ 5 16 * ]L > if
  0 tac-count !
    E5 note
  else
    1 tac-count +!
  then ;

[asm]
' bleep-slower
rom here swap
\ timer interrupt vector
$0050 ==> nop, nop, # call, reti,
( x ) ==>
[endasm]

: enable-timer-interrupt
  0 tac-count !
  TACF_START TACF_4KHZ or rTAC c! \ start timer, lowest rate 4096hz / 16 per sec
  IEF_TIMER rIE c@ or rIE c!      \ enable interrupts for timer
  0 rIF c! ;                      \ reset interrupts

\ require lib/time.fs
\ : bleep E5 note 200 ms  ;
\ : bleep-reti E5 note reti;

\ [asm]
\ ' bleep
\ rom here swap
\ \ input interrupt vector
\ \ $0060 ==> nop, nop, # jp,
\ $0060 ==> # call, reti,
\ ( x ) ==>
\ [endasm]

\ : enable-input-interrupt
\   IEF_HILO rIE c@ or rIE c!
\   $00 rP1 c!
\   0 rIF c! ;

: main
  install-font
  init-term
  $91 rLCDC c!

  \ enable-input-interrupt
  enable-timer-interrupt

  \ enabling the loop below breaks everything
  \ maybe the interrupt sequence interferes with
  \ the return stack somehow?

  \ begin
  \   page
  \   20 0 do [char] . emit loop
  \ again

  ;
