( IR -> Assembly)

require ./ir.fs
require ./code.fs
require ./xname.fs
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
: ir-node>addr ( node -- addr )
  ir-node-value @
  dup xprimitive? if
    >xcode emit-code
  else
    >xcode ir-addr @
  then ;

: ir-node>xcode ( node -- x )
  ir-node-value @ >xcode ;

: gen-call ( ir-node -- )
  ir-node>addr # call, ;

: gen-branch  ( ir-node -- )
  ir-node>addr # jp, ;

: gen-literal  ( ir-node -- )
  ir-node-value @ push-lit, ;

defer gen-ir

: gen-xname ( xname -- )
  dup xprimitive? if
    >xcode emit-code drop
  else
    >xcode gen-ir
  then ;

(
  This is called by gen-ir before emitting the main word, because you can not emit
  words while emitting another word: So this extra pass is needed.
  )
: gen-dependencies ( ir -- )
  do-nodes
    dup ir-node-type @ case
      IR_NODE_CALL   of dup ir-node-value @ gen-xname endof
      IR_NODE_BRANCH of dup ir-node-value @ gen-xname endof
    endcase
    next-node
  end-nodes ;

: gen-node ( ir-node -- )
  dup ir-node-type @ case
    IR_NODE_CALL    of gen-call    endof
    IR_NODE_LITERAL of gen-literal endof
    IR_NODE_BRANCH  of gen-branch  endof
    IR_NODE_RET     of drop ret,   endof
    true abort" (unknown node) "
  endcase ;

: gen-ir' ( ir -- )
  dup ir-addr @ if drop exit then
  dup gen-dependencies
  offset over ir-addr !
  do-nodes
    dup gen-node
    next-node
  end-nodes
; latestxt is gen-ir


( Get the address of code tokens and IR.

  It will ensure that the respective code is emitted, so the address
is known.
)

: code>addr ( code -- addr )
  emit-code ;

: ir>addr ( ir -- addr )
  dup gen-ir
  ir-addr @ ;

: xname>addr ( xname -- addr )
  dup xprimitive? if
    >xcode code>addr
  else
    >xcode ir>addr
  then ;


[endasm]
