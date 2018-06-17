( IR -> Assembly)

require ./ir.fs
require ./code.fs
require ./xname.fs
require ../asm.fs
require ../set.fs

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


defer gen-ir
defer gen-ir-component


[asm]

: push-lit,
  C dec,
  H A ld, A [C] ld,
  C dec,
  L A ld, A [C] ld,
  # HL ld, ;

: gen-call ( ir-node -- )
  ir-node>addr # call, ;

: gen-branch ( ir-node -- )
  ir-node>addr # jp, ;

: gen-literal ( ir-node -- )
  ir-node-value @ push-lit, ;

: separate-components? ( ir ir' -- flag )
  swap ir-topo-next @ <> ;


\ NOTE that if the referenced IR components in IR_NODE_CONTINUE
\ and IR_NODE_FORK are the next one in the topological order,
\ there is not need to compile a jump here because the other
\ component will physically followed this one in memory.

: gen-continue ( ir ir-node -- )
  2dup ir-node-value @ separate-components? if
    there> jp, ::fwd
  then
  2drop ;

: gen-fork ( ir ir-node -- )
  2dup ir-fork-consequent @ separate-components? if
    there> #z jp, ::fwd
  then
  2dup ir-fork-alternative @ separate-components? if
    there> #nz jp, ::fwd'
  then
  2drop ;

: gen-node ( ir ir-node -- )
  cr dup .node
  dup ir-node-type @ case
    IR_NODE_CALL     of nip gen-call    endof
    IR_NODE_LITERAL  of nip gen-literal endof
    IR_NODE_BRANCH   of nip gen-branch  endof
    IR_NODE_RET      of 2drop ret,      endof
    IR_NODE_CONTINUE of gen-continue    endof
    IR_NODE_FORK     of gen-fork        endof
    true abort" (Can't generate code for unknown IR node)"
  endcase ;

[endasm]


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
: gen-component-dependencies ( ir -- )
  do-nodes
    dup ir-node-type @ case
      IR_NODE_CALL   of dup ir-node-value @ gen-xname endof
      IR_NODE_BRANCH of dup ir-node-value @ gen-xname endof
    endcase
    next-node
  end-nodes ;

: gen-dependencies ( ir -- )
  ['] gen-component-dependencies pre-dfs traverse-components ;

: gen-ir-component' ( ir -- )
  cr ." --------------------------------" dup hex. cr
  
  offset over ir-addr !
  dup do-nodes
    2dup gen-node
    next-node
  end-nodes
  drop
; latestxt is gen-ir-component

: gen-ir' ( ir -- )
  dup ir-addr @ if drop exit then
  dup gen-dependencies
  ['] gen-ir-component toposort traverse-components
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
