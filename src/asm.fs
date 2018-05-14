( Game Boy assembler )

( This module implements an assembler for the Game Boy architecture

Each Assembly operand is represented as a single cell in the
stack. The Z80 architecture supports 8 and 16 bits operands, but for
the host assembly system we require words of at least 32 bits, so high
bits of the words are used to tag the values with type information. )

require ./utils.fs

[IFUNDEF] gb-assembler
vocabulary gb-assembler
vocabulary gb-assembler-emiters
[ENDIF]

get-current
also gb-assembler definitions
constant previous-wid

( You can override this vectored words in order to customize where the
( assembler will write its output )

defer emit ( value -- )
defer offset ( -- offset )
defer emit-to ( value offset -- )

variable counter
:noname hex. 1 counter +! ; is emit
:noname counter @ ; is offset
:noname 2drop ; is emit-to

: ensure-short-jr { e -- e }
  e -128 >= e 127 <= and
  invert abort" The relative jump is out of range"
  e ;


: emit-rel-to ( n source-offset -- )
  true abort" Implement me!" ;

: emit-16bits-to { n offset -- }
  n lower-byte   offset     emit-to
  n higher-byte  offset 1+  emit-to ;


( REFERENCES LIST )
(
  reflist
  +---+---+---+   +---+---+---+   +---+---+---+
  |   |   |  ---->|   |   |  ---->| 0 | 0 | 0 |
  +---+---+---+   +---+---+---+   +---+---+---+
                    |   \
                     \   ----- info
                 call \> xxxx

        [the address of ther eference]
)

struct
    cell% field fwdref-offset
    cell% field fwdref-info
    cell% field fwdref-next
end-struct fwdref%


$0 constant FWDREF_INFO_ABSOLUTE
$1 constant FWDREF_INFO_RELATIVE

: empty-reflist? ( reflist -- bool )
  fwdref-offset @ 0<> ;

: .reflist ( reflist -- )
  ." REFLIST:"
  begin dup empty-reflist? while
    ." " dup fwdref-offset @ hex.
    fwdref-next @
  repeat
  CR
  drop ;

: create-empty-reflist ( -- reflist )
  fwdref% %allot ;

: reflist-add ( offset info reflist -- reflist* )
  create-empty-reflist >r
  r@ fwdref-next !
  r@ fwdref-info !
  r@ fwdref-offset !
  r> ;

: patch-fwdref ( real-value fwdref -- )
  dup fwdref-info @ case
    FWDREF_INFO_ABSOLUTE of fwdref-offset @ emit-16bits-to endof
    FWDREF_INFO_RELATIVE of fwdref-offset @ emit-rel-to    endof
  endcase ;

: reflist-resolve ( real-value reflist -- )
  begin dup empty-reflist? while
    2dup patch-fwdref
    fwdref-next @
  repeat
  2drop ;


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

: begin-types 1 ;
: type dup constant 1 lshift ;
: end-types drop ;

begin-types
  type ~r
  type ~dd
  type ~qq
  type ~n
  type ~nn_
  type ~(n)
  type ~(nn)_
  type ~A
  type ~cc
  type ~unresolved-reference
end-types

: | or ;

: ~(nn) ~(n) ~(nn)_ | ;
: ~nn ~n ~nn_ | ;
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

%00 ~cc operand #NZ
%01 ~cc operand #Z
%10 ~cc operand #NC
%11 ~cc operand #C

( Push an immediate value to the arguments stack )
: #
  dup $FF <= if
    ~n push-arg
  else
    ~nn push-arg
  then ;

: ]*
  dup $FF00 >= if
    ~(n) push-arg
  else
    ~(nn) push-arg
  then ;


( LABEL & REFERENCES )

: presume
  create
  here
  0 , ~unresolved-reference ~nn | ,
  create-empty-reflist swap !
  does> dup cell+ @ push-arg ;

: redefine-label-forward ( xt -- )
  >body
  offset over !
  ~nn swap cell+ ! ;

: resolve-label-references ( xt -- )
  offset swap >body @ reflist-resolve ;

: fresh-label
  create offset , does> @ ~nn push-arg ;

: label
  parse-name
  2dup find-name ?dup if
    nip nip ( discard name )
    name>int
    dup resolve-label-references
    redefine-label-forward
  else
    nextname fresh-label
  then ;


( Arguments pattern matching )

: ` postpone postpone ; immediate

: type-match ( type type' -- bool )
  and 0<> ;

: 1arg-match? ( type -- bool )
  arg1-type type-match
  args# 1 = and ;

: 2arg-match? ( type1 type2 -- bool )
  arg2-type type-match swap
  arg1-type type-match and
  args# 2 = and ;

: args-match? ( ... #number-of-args -- bool )
  case
    1 of 1arg-match? endof
    2 of 2arg-match? endof
    abort" Unknown number of parameters"
  endcase ;


: begin-dispatch
  0
  ` depth
  ` >r
; immediate

: `#patterns ` depth ` r@ ` - ;

: ~~>
  1+ >r
  also gb-assembler-emiters
  `#patterns ` args-match? ` if
  r>
; immediate

: ::
  >r
  previous
  ` else
  r>
; immediate

: (unknown-args)
  flush-args
  true abort" Unknown parameters" ;

: end-dispatch
  ` (unknown-args)
  0 ?do ` then loop
  ` rdrop
; immediate


( Instructions helpers )

: reflist-add! ( value info &reflist -- )
  dup >r @ reflist-add r> ! ;

ALSO GB-ASSEMBLER-EMITERS
DEFINITIONS

: ..  3 lshift ;
: r  arg1-value | ;
: r' arg2-value | ;
: dd0' arg2-value 1 lshift | ;
: 0cc  arg1-value | ;
: 0cc' arg2-value | ;

:  8lit $ff and emit ;
: 16lit
  dup lower-byte  emit
      higher-byte emit ;

: emit-addr ( arg-value arg-type )
  dup ~unresolved-reference type-match if
    drop
    offset FWDREF_INFO_ABSOLUTE rot reflist-add!
    $4242 16lit
  else
    drop 16lit
  then ;

: emit-rel-addr ( arg-value arg-type )
  dup ~unresolved-reference type-match if
    drop
    offset 1+ FWDREF_INFO_RELATIVE rot reflist-add!
    $42 8lit
  else
    drop offset 1+ - ensure-short-jr 8lit
  then ;

: n   arg1-value  8lit ;
: n'  arg2-value  8lit ;
: e arg1-value arg1-type emit-rel-addr ;
: nn  arg1-value arg1-type emit-addr ;
: nn' arg2-value arg2-type emit-addr ;

PREVIOUS DEFINITIONS


: simple-instruction
  create , does> @ emit flush-args ;

: instruction :
  ` begin-dispatch ;

: end-instruction
  ` end-dispatch
  ` flush-args
  ` ;
; immediate



( INSTRUCTIONS )

instruction call,
  ~nn      ~~> %11001101             emit    nn  ::
  ~nn ~cc  ~~> %11 .. 0cc' .. %100 | emit    nn  ::
end-instruction

%11110011 simple-instruction di,
%11111011 simple-instruction ei,

( Bug in game boy forces us to emit a NOP after halt, because HALT has
  an inconsistent skipping of the next instruction depending on if the
  interruptions are enabled or not )
%01110110 simple-instruction halt%,


instruction jp,
  ~n ~~> %11000011 emit   n ::
end-instruction

instruction jr,
  ~nn ~~> %00011000 emit   e ::
end-instruction

instruction ld,
  ~r   ~r   ~~> %01 .. r'   .. r      emit      ::
  ~n   ~r   ~~> %00 .. r'   .. %110 | emit   n  ::
  ~nn ~dd   ~~> %00 .. dd0' .. %001 | emit  nn  ::

  ~(n) ~A   ~~> %11110000 emit    n  ::
  ~A   ~(n) ~~> %11100000 emit    n' ::
end-instruction

%00000000 simple-instruction nop,

instruction ret,
        ~cc ~~> %11 .. 0cc .. 000 | emit   ::
end-instruction

%00000111 simple-instruction rlca,

: stop,
  %00010000 emit
  %00000000 emit
  flush-args ;

( Prevent the halt bug by emitting a NOP right after halt )
: halt, halt%, nop, ;


previous-wid set-current
previous
