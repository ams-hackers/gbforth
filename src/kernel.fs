( A DIRECT-THREADED KERNEL FOR DMG-FORTH )

require ./asm.fs

also gb-assembler

: .start
  rom-offset
  countcycles @ ;

: .end
  countcycles @ swap - >r
  rom-offset swap - r>
  ." Cycles: " . CR
  ." Bytes: " . CR ;


( TEMPORARY HACK: Don't break the game! )
$4400 ==>


( Virtual Registers

  W: Working register. _Can be used as a scratch register_. At the
     beginning of each word, it is initialized to the code address of
     the current word.

  IP: Instruction pointer. Address of the current word being executed.

  PSP: Parameter stack pointer. Grows downward.

  RSP: Return stack pointer. Grows downward.

  Those registers are mapped into the Game boy hardware registers as
  follows

       W = HL
      IP = DE
     RSP = BC
     PSP = SP
)

: W HL ;
: [W] [HL] ;
: IP DE ;
: [IP] [DE] ;

( Assume you have the following code

  : double dup + ;
  : quadruple double double ;


  This kernel uses a classic direct-threading Forth. The picture below
  illustrates how the compiled words would look like:

                    +------
                    |   ...                       DUP
                    +------
                       ^
                       |
       +----------+-----+-----+------+
       | ENTER jp | dup |  +  | exit |             DOUBLE
       +----------+-----+-----+------+
            ^   
             \________
                      \
       +----------+--------+--------+------+
       | ENTER jp | double | double | exit |       QUADRUPLE
       +----------+--------+--------+------+
       code field |       data field
                  |        

  Each word is compiled into a _code field_ and an optional _data
  field_.

  The code field contains executable code for the word. The data field
  contains auxiliary data that the code field could use.

  For example, for all colon definitions, the code field is the ENTER
  routine, and the data field is an array of the words [code fields]
  to execute. 
)


( NEXT is the entrypoint of the inner interpreter. It will execute a
  single word by jumping to its code field and update IP to point to
  the next word.

  The Forth register W is initialized to the code field of the word to
  execute.
)

: [IP++]->W
  [DE] A ld, A L ld,
  DE INC,
  [DE] A ld, A H ld,
  DE INC, ;

: next,                         ( 11 cycles / 7 bytes )
  [IP++]->W
  [W] jp, ;

: code
  rom-offset constant ;


code next
next,


( ENTER & EXIT

  This routines reinitailize the virtual machine to execute the list
  of words in the data field.

  Before we can do so, we have to save the current IP in the current
  stack so we can jump back later.
)

: push-ip-to-return-stack
  BC dec, 
  D A ld, A [BC] ld,
  BC dec,
  E A ld, A [BC] ld, ;

: pop-ip-from-return-stack
  [BC] A ld, A E ld,
  BC inc,
  [BC] A ld, A D ld,
  BC inc, ;

: HL->DE
  H D ld,
  L E ld, ;

: W->IP HL->DE ;

: set-ip-first-word-in-data-field
  W->IP
  IP inc,
  IP inc,
  IP inc, ;

code enter
push-ip-to-return-stack
set-ip-first-word-in-data-field
next # jp,

code exit
pop-ip-from-return-stack
next # jp,


( Code Words

  Those are primitive [code] words written in dmg-forth.
)

[user-definitions]

: next next ;

code dup
HL pop,
HL push,
HL push,

code double
enter # jp,
dup 16lit,
exit 16lit,

[end-user-definitions]


previous
