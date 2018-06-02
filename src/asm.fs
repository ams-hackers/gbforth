( Game Boy assembler )

( This module implements an assembler for the Game Boy architecture

Each Assembly operand is represented as a single cell in the
stack. The Z80 architecture supports 8 and 16 bits operands, but for
the host assembly system we require words of at least 32 bits, so high
bits of the words are used to tag the values with type information. )

require ./utils/bytes.fs
require ./sym.fs

[IFUNDEF] gb-assembler-impl
vocabulary gb-assembler
vocabulary gb-assembler-impl
vocabulary gb-assembler-emiters
[ENDIF]

: [asm] also gb-assembler ;
: [endasm] previous ;

get-current
also gb-assembler
also gb-assembler-impl definitions
constant previous-wid

: [public]
  get-current
  also gb-assembler definitions ;

: [endpublic]
  previous set-current ;


( You can override this vectored words in order to customize where the
( assembler will write its output )

defer emit ( value -- )
defer offset ( -- offset )
defer emit-to ( value offset -- )

variable counter
:noname hex. 1 counter +! ; is emit
:noname counter @ ; is offset
:noname 2drop ; is emit-to

variable countcycles

: ensure-short-jr { e -- e }
  e -128 >= e 127 <= and
  invert abort" The relative jump is out of range"
  e ;


: emit-rel-to ( n source-offset -- )
  dup >r 1+ - r> emit-to ;

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
  type ~HL
  type ~SP
  type ~(BC)
  type ~(DE)
  type ~(HL)
  type ~(HL+)
  type ~(HL-)
  type ~(C)
  type ~b
  type ~n
  type ~nn
  type ~(n)
  type ~(nn)
  type ~A
  type ~cc
  type ~unresolved-reference
end-types

: ~e ~nn ;

: | or ;

: ~ss ~dd ;
: ~qq|dd ~dd ~qq | ;

: operand ( value type )
  create , , does> 2@ push-arg ;

[public]

( Define register operands )
%111 ~r ~A | operand A
%000 ~r      operand B
%001 ~r      operand C
%010 ~r      operand D
%011 ~r      operand E
%100 ~r      operand H
%101 ~r      operand L

%00 ~qq|dd operand BC
%01 ~qq|dd operand DE
%10 ~qq|dd ~HL | operand HL
%11    ~dd ~SP | operand SP
%11 ~qq    operand AF

%00 ~cc operand #NZ
%01 ~cc operand #Z
%10 ~cc operand #NC
%11 ~cc operand #C

%0   ~(BC)  operand [BC]
%0   ~(DE)  operand [DE]
%110 ~(HL)  operand [HL]
%0   ~(HL+) operand [HL+]
%0   ~(HL-) operand [HL-]
%0   ~(C)   operand [C]

( Push an immediate value to the arguments stack )
: #
  dup %111 <= if
    ~b ~n | ~nn | push-arg
  else
    dup $FF <= if
      ~n ~nn | push-arg
    else
      ~nn push-arg
    then
  then ;

: ]*
  dup $FF00 >= if
    ~(n) ~(nn) | push-arg
  else
    ~(nn) push-arg
  then ;


[endpublic]



( LABEL & REFERENCES )

[public]

: here< offset ;
: <there # ;

: presume
  create
  here
  0 , ~unresolved-reference ~nn | ,
  create-empty-reflist swap !
  does> dup cell+ @ push-arg ;

[endpublic]


: redefine-label-forward ( xt -- )
  >body
  offset over !
  ~nn swap cell+ ! ;

: resolve-label-references ( xt -- )
  offset swap >body @ reflist-resolve ;

[public]
: newlabel
  create offset , does> @ ~nn push-arg ;
[endpublic]

: make-label ( c-addr u -- )
  2dup offset sym
  2dup find-name ?dup if
    nip nip ( discard name )
    name>int
    dup resolve-label-references
    redefine-label-forward
  else
    nextname newlabel
  then ;

false value start-defined?
false value main-defined?

[public]
: label
  parse-name make-label ;

: __start:
  s" __start" make-label
  true is start-defined? ;

: main:
  s" main" make-label
  true is main-defined? ;
[endpublic]


[public]
( Utility words to define local labels )
: local ( -- wid )
  get-current
  wordlist
  dup >order
  set-current immediate ;

: end-local
  previous
  set-current immediate ;

[endpublic]


( Arguments pattern matching )

: ` postpone postpone ; immediate

: type-match ( type type' -- bool )
  and 0<> ;

: 0arg-match? ( -- bool )
  args# 0 = ;

: 1arg-match? ( type -- bool )
  arg1-type type-match
  args# 1 = and ;

: 2arg-match? ( type1 type2 -- bool )
  arg2-type type-match swap
  arg1-type type-match and
  args# 2 = and ;

: args-match? ( ... #number-of-args -- bool )
  case
    0 of 0arg-match? endof
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

: ..  3 lshift ;
: op { prefix tribble1 tribble2 -- opcode }
  prefix .. tribble1 | .. tribble2 | ;

:  8lit, $ff and emit ;
: 16lit,
  dup lower-byte  emit
      higher-byte emit ;

ALSO GB-ASSEMBLER-EMITERS
DEFINITIONS

: b' arg2-value ;
: r  arg1-value ;
: r' arg2-value ;
: dd0' arg2-value 1 lshift ;
: 0cc  arg1-value ;
: 0cc' arg2-value ;
: 1cc' 0cc' %100 + ;
: ss0 arg1-value 1 lshift ;
: ss1 ss0 %1 | ;
: qq0 arg1-value 1 lshift ;

: emit-addr ( arg-value arg-type )
  dup ~unresolved-reference type-match if
    drop
    offset FWDREF_INFO_ABSOLUTE rot reflist-add!
    $4242 16lit,
  else
    drop 16lit,
  then ;

: emit-rel-addr ( arg-value arg-type )
  dup ~unresolved-reference type-match if
    drop
    offset FWDREF_INFO_RELATIVE rot reflist-add!
    $42 8lit,
  else
    drop offset 1+ - ensure-short-jr 8lit,
  then ;

: n,   arg1-value 8lit, ;
: n',  arg2-value 8lit, ;
: e,   arg1-value arg1-type emit-rel-addr ;
: nn,  arg1-value arg1-type emit-addr ;
: nn', arg2-value arg2-type emit-addr ;

: op, op emit ;
: cb-op, $cb emit op emit ;

PREVIOUS DEFINITIONS

: instruction :
  ` begin-dispatch ;

: end-instruction
  ` end-dispatch
  ` flush-args
  ` ;
; immediate


( INSTRUCTIONS )

: cycles countcycles +! ;

[public]

' countcycles alias countcycles

: 16lit, 16lit, ;
: 8lit,  8lit, ;

instruction adc,
  ~r    ~A  ~~> %10 %001    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %001 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %001 %110 op, n,    2 cycles ::
end-instruction

instruction add,
  ~r    ~A  ~~> %10 %000    r op,       1 cycles ::
  ~n    ~A  ~~> %11 %000 %110 op, n,    2 cycles ::
  ~(HL) ~A  ~~> %10 %000 %110 op,       2 cycles ::
  ~ss   ~HL ~~> %00 ss1  %001 op,       2 cycles ::
  ~e    ~SP ~~> %11 %101 %000 op, e,    4 cycles ::
end-instruction

instruction and,
  ~r    ~A  ~~> %10 %100    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %100 %110 op,       2 cycles ::
  ~n        ~~> %11 %100 %110 op, n,    2 cycles ::
end-instruction

instruction call,
  ~nn       ~~> %11 %001 %101 op, nn,   6 cycles ::
  ~nn ~cc   ~~> %11 0cc' %100 op, nn,   6 cycles :: ( 3 if false )
end-instruction

instruction cp,
  ~r    ~A  ~~> %10 %111    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %111 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %111 %110 op, n,    2 cycles ::
end-instruction

instruction dec,
  ~r        ~~> %00 r    %101 op,       1 cycles ::
  ~(HL)     ~~> %00 %110 %101 op,       3 cycles ::
  ~ss       ~~> %00 ss1  %011 op,       2 cycles ::
end-instruction

instruction di,
            ~~> %11 %110 %011 op,       1 cycles ::
end-instruction

instruction ei,
            ~~> %11 %111 %011 op,       1 cycles ::
end-instruction

( Bug in game boy forces us to emit a NOP after halt, because HALT has
  an inconsistent skipping of the next instruction depending on if the
  interruptions are enabled or not )
instruction halt%,
            ~~> %01 %110 %110 op,       1 cycles ::
end-instruction

instruction inc,
  ~r        ~~> %00   r  %100 op,       1 cycles ::
  ~(HL)     ~~> %00 %110 %100 op,       3 cycles ::
  ~ss       ~~> %00 ss0  %011 op,       2 cycles ::
end-instruction

instruction jp,
  ~nn       ~~> %11 %000 %011 op, nn,   4 cycles ::
  ~nn  ~cc  ~~> %11 0cc' %010 op, nn,   4 cycles :: ( 3 if false )
  ~(HL)     ~~> %11 %101 %001 op,       1 cycles ::
end-instruction

instruction jr,
  ~e        ~~> %00 %011 %000 op, e,    3 cycles ::
  ~e ~cc    ~~> %00 1cc' %000 op, e,    3 cycles ::
end-instruction

instruction ld,
  ~r   ~r   ~~> %01 r'   r    op,       1 cycles ::
  ~n   ~r   ~~> %00 r'   %110 op, n,    2 cycles ::

  ~(HL) ~r  ~~> %01   r' %110 op,       2 cycles ::
  ~r  ~(HL) ~~> %01 %110    r op,       2 cycles ::
  ~n  ~(HL) ~~> %00 %110 %110 op, n,    3 cycles ::

  ~(BC) ~A  ~~> %00 %001 %010 op,       2 cycles ::
  ~(DE) ~A  ~~> %00 %011 %010 op,       2 cycles ::

  ~(C) ~A   ~~> %11 %110 %010 op,       2 cycles ::
  ~A  ~(C)  ~~> %11 %100 %010 op,       2 cycles ::

  ~(n) ~A   ~~> %11 %110 %000 op, n,    3 cycles ::
  ~A   ~(n) ~~> %11 %100 %000 op, n',   3 cycles ::
  ~(nn) ~A  ~~> %11 %111 %010 op, nn,   4 cycles ::
  ~A  ~(nn) ~~> %11 %101 %010 op, nn',  4 cycles ::

  ~(HL+) ~A ~~> %00 %101 %010 op,       2 cycles ::
  ~(HL-) ~A ~~> %00 %111 %010 op,       2 cycles ::

  ~A  ~(BC) ~~> %00 %000 %010 op,       2 cycles ::
  ~A  ~(DE) ~~> %00 %010 %010 op,       2 cycles ::

  ~A ~(HL+) ~~> %00 %100 %010 op,       2 cycles ::
  ~A ~(HL-) ~~> %00 %110 %010 op,       2 cycles ::

  ~nn  ~dd  ~~> %00 dd0' %001 op, nn,   3 cycles ::

  ~HL  ~SP  ~~> %11 %111 %001 op,       2 cycles ::

  ~e   ~HL  ~~> %11 %111 %000 op, e,    3 cycles :: \ equal to ldhl,

  ~SP ~(nn) ~~> %00 %001 %000 op, nn',  5 cycles ::
end-instruction

instruction ldhl,
  ~e   ~SP  ~~> %11 %111 %000 op, e,    3 cycles ::
end-instruction

instruction nop,
            ~~> %00 %000 %000 op,       1 cycles ::
end-instruction

instruction or,
  ~r    ~A  ~~> %10 %110    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %110 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %110 %110 op, n,    2 cycles ::
end-instruction

instruction pop,
  ~qq       ~~> %11 qq0 %001 op,        3 cycles ::
end-instruction

instruction push,
  ~qq       ~~> %11 qq0 %101 op,        4 cycles ::
end-instruction

instruction res,
  ~r ~b     ~~>  %11 %001 %011 op,
                 %10   b'    r op,      2 cycles ::
end-instruction

instruction ret,
        ~cc ~~> %11  0cc %000 op,       4 cycles ::
            ~~> %11 %001 %001 op,       4 cycles ::
end-instruction

instruction reti,
            ~~> %11 %011 %001 op,       4 cycles ::
end-instruction

instruction rl,
  ~r        ~~> %00 %010    r cb-op,    2 cycles ::
  ~(HL)     ~~> %00 %000 %110 cb-op,    4 cycles ::
end-instruction

instruction rlca,
            ~~> %00 %000 %111 op,       1 cycles ::
end-instruction

instruction sbc,
  ~r    ~A  ~~> %10 %011    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %011 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %011 %110 op, n,    2 cycles ::
end-instruction

instruction stop,
            ~~> %00 %010 %000 op,
                %00 %000 %000 op,       1 cycles ::
end-instruction

instruction sub,
  ~r    ~A  ~~> %10 %010    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %010 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %010 %110 op, n,    2 cycles ::
end-instruction

instruction xor,
  ~r    ~A  ~~> %10 %101    r op,       1 cycles ::
  ~(HL) ~A  ~~> %10 %101 %110 op,       2 cycles ::
  ~n    ~A  ~~> %11 %101 %110 op, n,    2 cycles ::
end-instruction

( Prevent the halt bug by emitting a NOP right after halt )
: halt, halt%, nop, ;

[endpublic]

previous-wid set-current
previous
