( Game Boy assembler )

( This module implements an assembler for the Game Boy architecture

Each Assembly operand is represented as a single cell in the
stack. The Z80 architecture supports 8 and 16 bits operands, but for
the host assembly system we require words of at least 32 bits, so high
bits of the words are used to tag the values with type information. )

require ./utils.fs

[IFUNDEF] GB-ASSEMBLER
VOCABULARY GB-ASSEMBLER
[ENDIF]

ALSO GB-ASSEMBLER DEFINITIONS

( INSTRUCTION ARGUMENTS STACK

Instruction arguments are represented with two words [ value type ].

We define an auxiliary stack to hold the arguments for the next
aasembly instruction. Having a dedicate stack has some benefits like,
we will be able to override the instruction based on the number of
arguments provided. )

2 cells constant arg-size

create args 2 arg-size * allot
0 value args#

: flush-args 0 to args# ;

: check-full-args
  args# 1 > abort" Too many arguments for an assembly instruction." ;

: push-arg ( value type -- )
  check-full-args
  swap args# arg-size * args + 2!
  args# 1+ to args# ;

( Those words allow us to extract the argument value and type for the
  current instruction )
: arg1-value args 0 cells + @ ;
: arg1-type  args 1 cells + @ ;
: arg2-value args 2 cells + @ ;
: arg2-type  args 3 cells + @ ;

( for debugging )
: .args
  ." args# = " args# . CR
  ." arg1-value " arg1-value . CR
  ." arg1-type  " arg1-type  . CR
  ." arg2-value " arg2-value . CR
  ." arg2-type  " arg2-type  . CR ;



( Argument Types )
%0000001 constant ~r
%0000010 constant ~dd
%0000100 constant ~qq
%0001000 constant ~imm
%0110000 constant ~(n)          ( overlaps with ~nn )
%0100000 constant ~(nn)
%1000000 constant ~A

: | or ;

: ~qq|dd ~dd ~qq | ;

: operand ( value type )
  create , , does> 2@ push-arg ;

( Define register operands )
%111 ~r ~A | operand A
%000 ~r      operand B
%001 ~r      operand C
%101 ~r      operand D
%011 ~r      operand E
%100 ~r      operand H
%101 ~r      operand L

%00 ~qq|dd operand BC
%01 ~qq|dd operand DE
%10 ~qq|dd operand HL
%11    ~dd operand SP
%11 ~qq    operand AF

( Push an immediate value to the arguments stack )
: # ~imm push-arg ;
: ]*
  dup $FF00 >= if
    ~(n) push-arg
  else
    ~(nn) push-arg
  then ;

( Arguments pattern matching )

: ` postpone postpone ; immediate

: type-match ( type type' -- bool )
  and 0<> ;

: 2arg-match? ( type1 type2 -- bool )
  arg2-type type-match swap
  arg1-type type-match and
  args# 2 = and ;

: begin-dispatch
  0
; immediate

: ~~>
  1+ >r
  ` 2arg-match? ` if
  r>
; immediate

: ::
  >r
  ` else 
  r>
; immediate

: (unknown-args)
  flush-args
  true abort" Unknown parameters" ;

: end-dispatch
  ` (unknown-args)
  0 ?do ` then loop
; immediate
  
defer emit
' hex. is emit 


: ..  6 rshift ;
: r  arg2-value 3 lshift | ;
: r' arg1-value | ;
: dd0 arg2-value 4 lshift | ;

:  8lit $ff and emit ;
: 16lit 
  dup lower-byte  emit
      higher-byte emit ;

: n   arg1-value  8lit ;
: nn  arg1-value 16lit ;
: n'  arg2-value  8lit ;
: nn' arg2-value 16lit ;


: ld,
  begin-dispatch
  ~r   ~r   ~~> %01 .. r   r'      emit     ::
  ~imm ~r   ~~> %00 .. r   %110  | emit  n  ::
  ~imm ~dd  ~~> %00 .. dd0 %001  | emit nn  ::

  ~(n) ~A   ~~>      %11110000     emit  n  ::
  ~A   ~(n) ~~>      %11100000     emit  n' ::

  end-dispatch
  flush-args ;

: nop,   %00000000 emit ;
: di,    %11110011 emit ;
: ei,    %11111011 emit ; 
: halt%, %01110110 emit ;

( Bug in game boy forces us to emit a NOP after halt, because HALT has
  an inconsistent skipping of the next instruction depending on if the
  interruptions are enabled or not ) 
: halt, halt%, nop, ;

: stop,
  %00010000 emit
  %00000000 emit ;



\ ld 8reg 8imm        00 r   110      n

\ ld (hl) imm8        00 110 110      n

\ ld A (bc)           00 001 010
\ ld A (de)           00 011 010

\ ld A (hli)          00 101 010
\ ld A (hld)          00 111 010

\ ld (bc) A           00 000 010
\ ld (de) A           00 010 010
\ ld (hli) A          00 100 010
\ ld (hld) A          00 110 010

\ ld 8reg 8reg        01 r r'

\ ld 8reg (hl)        01 r   110
\ ld (hl) 8reg        01 110 r



\ ld A (C)            11 110 010
\ ld (C) A            11 100 010

\ ld A (n) FFnn -> A  11 110 000      n
\ ld (n) A            11 100 000      n

\ ld A (nn)           11 111 010      n    n
\ ld (nn) A           11 101 010      n    n


\ n
\ CASE
\   n1 OF code1 ENDOF
\   n2 OF code2 ENDOF
\   ...
\   ( n ) default-code ( n )
\ ENDCASE ( )

PREVIOUS DEFINITIONS
