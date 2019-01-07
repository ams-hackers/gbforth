title: SYNTH
makercode: TK

\ Simple but complete synthesiser for the first sound channel (SQUARE 1)
\
\ [up] / [down] to change selection
\ [left] / [right] to decrease/increase the selected value by 1
\ [select] / [start] to decrease/increase the selected value by 10
\ [B] Play the sound (using the LENGTH value)
\ [A] Play the sound (ignore the length)
\
\ On the bottom of the screen you can see the values that
\ are written to the NR10-NR14 registers. You can copy over
\ these values to your own game to generate the same sound.
\
\ For NR14, the first value corresponds to the sound as produced with [B]
\ the second value corresponds to the sound produced by pressing [A]
\
\ If NR10 is `00` (you are not using the SWEEP/NEGATE/SHIFT parameters)
\ you can also use the second sound channel (SQUARE 2) to produce the same sound
\ by using the registers NR21-NR24 in your game.

require sfx.fs
require gbhw.fs
require input.fs
require term.fs
require ibm-font.fs

\ these parameters hold the values for NR10-NR14
variable param-a
variable param-b
variable param-c
variable param-d
variable param-e

\ cursor Y position
variable current

: mask-offset
  0 swap
  begin
  dup %1 and 0= while
    swap 1+ swap
    1 rshift
  repeat
  drop ;

: get-mask ( abc 110 -- ab )
  tuck and
  swap mask-offset rshift ;

: set-mask ( abc xyz 101 -- xbz )
  tuck
  mask-offset lshift
  over and
  -rot
  invert and
  or ;

\  NR10 [-PPP NSSS] Sweep period, negate, shift
%01110000 constant &sweep
%00001000 constant &neg
%00000111 constant &shift

: sqr-sweep@ param-a c@ &sweep get-mask ;
: sqr-sweep! param-a c@ swap &sweep set-mask param-a c! ;
: sqr-neg@ param-a c@ &neg get-mask ;
: sqr-neg! param-a c@ swap &neg set-mask param-a c! ;
: sqr-shift@ param-a c@ &shift get-mask ;
: sqr-shift! param-a c@ swap &shift set-mask param-a c! ;

\  NR11 [DDLL LLLL] Duty, Length load (64-L)
%11000000 constant &duty
%00111111 constant &len

: sqr-duty@ param-b c@ &duty get-mask ;
: sqr-duty! param-b c@ swap &duty set-mask param-b c! ;
: sqr-len@ param-b c@ &len get-mask ;
: sqr-len! param-b c@ swap &len set-mask param-b c! ;

\  NR12 [VVVV APPP] Starting volume, Envelope add mode, period
%11110000 constant &vol
%00001000 constant &env-mode
%00000111 constant &period

: sqr-vol@ param-c c@ &vol get-mask ;
: sqr-vol! param-c c@ swap &vol set-mask param-c c! ;
: sqr-env-mode@ param-c c@ &env-mode get-mask ;
: sqr-env-mode! param-c c@ swap &env-mode set-mask param-c c! ;
: sqr-period@ param-c c@ &period get-mask ;
: sqr-period! param-c c@ swap &period set-mask param-c c! ;

\  NR13 [FFFF FFFF] Frequency LSB
\  NR14 [TL-- -FFF] Trigger, Length enable, Frequency MSB
%11111111 constant &freq-d
%00000111 constant &freq-e

: sqr-freq-d@ param-d c@ &freq-d get-mask ;
: sqr-freq-e@ param-e c@ &freq-e get-mask ;
: sqr-freq@ sqr-freq-e@ 8 lshift sqr-freq-d@ + ;
: sqr-freq-d! param-d c@ swap &freq-d set-mask param-d c! ;
: sqr-freq-e! param-e c@ swap &freq-e set-mask param-e c! ;
: sqr-freq!
  dup
  %00011111111 and sqr-freq-d!
  %11100000000 and 8 rshift sqr-freq-e! ;

: current-param@
  current c@ case
    0 of sqr-sweep@ endof
    1 of sqr-neg@ endof
    2 of sqr-shift@ endof
    3 of sqr-duty@ endof
    4 of sqr-len@ endof
    5 of sqr-vol@ endof
    6 of sqr-env-mode@ endof
    7 of sqr-period@ endof
    8 of sqr-freq@ endof
  endcase ;

: current-param-mod
  current c@ case
    0 of 8 + 8 mod endof
    1 of 2 + 2 mod endof
    2 of 8 + 8 mod endof
    3 of 4 + 4 mod endof
    4 of 64 + 64 mod endof
    5 of 16 + 16 mod endof
    6 of 2 + 2 mod endof
    7 of 8 + 8 mod endof
    8 of 2048 + 2048 mod endof
  endcase ;

: current-param!
  current c@ case
    0 of sqr-sweep! endof
    1 of sqr-neg! endof
    2 of sqr-shift! endof
    3 of sqr-duty! endof
    4 of sqr-len! endof
    5 of sqr-vol! endof
    6 of sqr-env-mode! endof
    7 of sqr-period! endof
    8 of sqr-freq! endof
  endcase ;

: .hexdigit
  dup 10 < if
    [char] 0 + emit
  else
    case
      10 of [char] A emit endof
      11 of [char] B emit endof
      12 of [char] C emit endof
      13 of [char] D emit endof
      14 of [char] E emit endof
      15 of [char] F emit endof
    endcase
  then ;

: without-len %10111111 and ;
: with-len %01000000 or ;

: .hex
  16 /mod .hexdigit .hexdigit ;

: .ui
  0 0  at-xy ." Square 1"

  1 2  at-xy ." Sweep period:" space sqr-sweep@ .
  1 3  at-xy ." Negate:      " space sqr-neg@ .
  1 4  at-xy ." Shift:       " space sqr-shift@ .

  1 5  at-xy ." Duty:        " space sqr-duty@ .
  1 6  at-xy ." Length load: " space sqr-len@ .

  1 7  at-xy ." Start volume:" space sqr-vol@ .
  1 8  at-xy ." ENV add mode:" space sqr-env-mode@ .
  1 9  at-xy ." Period:      " space sqr-period@ .

  1 10 at-xy ." Frequency:   " space sqr-freq@ .

  \ display the pointer
  0 current @ 2 + at-xy [char] > emit

  0 12 at-xy ." --------------------"
  5 13 at-xy ." NR10:" space param-a c@ .hex
  5 14 at-xy ." NR11:" space param-b c@ .hex
  5 15 at-xy ." NR12:" space param-c c@ .hex
  5 16 at-xy ." NR13:" space param-d c@ .hex
  5 17 at-xy ." NR14:" space
    param-e c@ with-len .hex
    space [char] / emit space
    param-e c@ without-len .hex
  ;

: play-sound
  param-a c@ rNR10 c!
  param-b c@ rNR11 c!
  param-c c@ rNR12 c!
  param-d c@ rNR13 c!
  param-e c@ without-len rNR14 c! ;

: play-sound-len
  param-a c@ rNR10 c!
  param-b c@ rNR11 c!
  param-c c@ rNR12 c!
  param-d c@ rNR13 c!
  param-e c@ with-len rNR14 c! ;

: handle-input
  begin
    key case
      k-down   of current c@ 1 + 9 mod current c!                      endof
      k-up     of current c@ 8 + 9 mod current c!                      endof
      k-right  of current-param@ 1 + current-param-mod current-param!  endof
      k-left   of current-param@ 1 - current-param-mod current-param!  endof
      k-start  of current-param@ 10 + current-param-mod current-param! endof
      k-select of current-param@ 10 - current-param-mod current-param! endof
      k-a      of play-sound                                           endof
      k-b      of play-sound-len                                       endof
    endcase
    page .ui
  again ;

: main
  install-font
  init-term
  init-input
  init-sfx

  $15 param-a c!
  $96 param-b c!
  $73 param-c c!
  $BB param-d c!
  $85 param-e c!

  0 current !

  page .ui
  handle-input ;
