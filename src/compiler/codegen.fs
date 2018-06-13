( IR -> Assembly)

require ./ir.fs
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

\ resolves offset from both primitive and non-primitive nodes
: ir-node>addr
  dup ir-node-value @ swap
  primitive? invert if ir-addr @ then ;

: gen-call ( ir-node -- )
  ir-node>addr # call, ;

: gen-branch  ( ir-node -- )
  ir-node>addr # jp, ;

: gen-literal  ( ir-node -- )
  ir-node-value @ push-lit, ;

defer gen-code

(
  This is called by gen-code before emitting the main word, because you can not emit
  words while emitting another word: So this extra pass is needed.
  )
: gen-dependencies ( ir -- )
  ir-entry
  begin ?dup while
    dup primitive? invert if
      dup ir-node-type @ case
        IR_NODE_CALL   of dup ir-node-value @ gen-code endof
        IR_NODE_BRANCH of dup ir-node-value @ gen-code endof
      endcase
    then
    next-node
  repeat ;

: gen-node ( ir-node -- )
  dup ir-node-type @ case
    IR_NODE_CALL    of gen-call    endof
    IR_NODE_LITERAL of gen-literal endof
    IR_NODE_BRANCH  of gen-branch  endof
    IR_NODE_RET     of drop ret,   endof
    true abort" (unknown node) "
  endcase ;

: gen-code' ( ir -- )
  dup ir-addr @ if drop exit then
  dup gen-dependencies
  offset over ir-addr !
  ir-entry
  begin ?dup while
    dup gen-node
    next-node
  repeat
; latestxt is gen-code


[endasm]
