( Game Boy assembler )

( This module implements an assembler for the Game Boy architecture

Each Assembly operand is represented as a single cell in the
stack. The Z80 architecture supports 8 and 16 bits operands, but for
the host assembly system we require words of at least 32 bits, so high
bits of the words are used to tag the values with type information. )

( Instruction operands  )

%0001 constant ~r
%0010 constant ~dd
%0100 constant ~qq
%1000 constant ~imm

: ~qq|dd ~dd ~qq or ;

: operand ( value type )
  create , , does> 2@ ;

%111 ~r operand A
%000 ~r operand B
%001 ~r operand C
%101 ~r operand D
%011 ~r operand E
%100 ~r operand H
%101 ~r operand L

%00 ~qq|dd operand BC
%01 ~qq|dd operand DE
%10 ~qq|dd operand HL
%11    ~dd operand SP
%11 ~qq    operand AF

( Mark the value on the stack as an immediate value )
: # ~imm ;

: istype? ( value type type -- bool )
  and 0<> swap drop ;

: duptype ( value type -- value type type )
  dup ;

: dup2types
  2 pick over ;


( Instructions )

: emit hex. ;

: type-match? ( type1 type2 -- bool )
  and 0<> ;

: 2types-match? { type1 type2 type1' type2' -- bool }
  type1 type1' type-match? >r
  type2 type2' type-match? r>
  and ;

: op-2drop 2drop 2drop ;


: dispatch
  postpone dup2types
  postpone 2>r
; immediate

: ~>
  postpone 2r@
  postpone 2types-match?
  postpone if
; immediate

: end-dispatch
  postpone 2rdrop
; immediate
  

: ld, ( from from-type to to-type -- )
  dispatch
  
  ~r ~r ~>
    op-2drop
    ." r -> r "
  else

    ~imm ~r ~>
      op-2drop
      ." imm -> r "
    else
      
      2rdrop
      true abort" Unknown parameters to LD"
      
    then
  then

  end-dispatch
;



: nop,   %00000000 emit ;

: di,    %11110011 emit ;
: ei,    %11111011 emit ; 

: halt%,  %01110110 emit ;
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
