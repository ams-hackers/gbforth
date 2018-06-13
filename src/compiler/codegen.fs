( IR -> Assembly)

require ../asm.fs
[asm]

( Assume you have the following code

  : double dup + ;
  : quadruple double double ;

  This kernel uses subroutine-threading Forth. The picture below
  illustrates how the compiled words would look like:

              +------
              |  ...                            DUP [asm]
              +------
                ^
                |
               /
       +----------+--------+------+
       | CALL dup | CALL + | RET  |             DOUBLE
       +----------+--------+------+
         ^
         \____________________
                              \
       +-------------+-------------+-----+
       | CALL double | CALL double | RET |      QUADRUPLE
       +-------------+-------------+-----+

  For all colon definitions, the code field simply contains CALLs to
  every word [or primitive] address that is part of the word definition.
)


: push-lit,
  C dec,
  H A ld, A [C] ld,
  C dec,
  L A ld, A [C] ld,
  # HL ld, ;

( Words used by the cross compiler )

: gen-node ( ir-node -- )
  dup ir-node-value @
  swap ir-node-type @ case
    IR_NODE_CALL    of # call,      endof
    IR_NODE_LITERAL of push-lit,    endof
    IR_NODE_BRANCH  of # jp,        endof
    IR_NODE_RET     of drop ret,    endof
    true abort" (unknown node) "
  endcase ;

: gen-code ( ir -- )
  ir-entry
  begin ?dup while
    dup gen-node
    next-node
  repeat ;


[endasm]
