( Intermedian Representation [IR]

  As an intermediate representation for the compiler, we use a doubly
linked-list of ir-node% elements. This allows us to easily insert and
delete nodes.  )

1 constant IR_NODE_CALL
2 constant IR_NODE_LITERAL
3 constant IR_NODE_RET
4 constant IR_NODE_BRANCH

struct
\ The ir-node-next field must be the first field of this struct, as it
\ is shared by the ir% struct.
  cell% field ir-node-next
  cell% field ir-node-prev
  cell% field ir-node-type
  cell% field ir-node-value
end-struct ir-node%

struct
\ The ir-entry field must be the first field of this struct, as it is
\ shared by the ir-node% struct. In that sense, we use the ir% struct
\ as a sentinel node.
  cell% field ir-entry%
end-struct ir%

: previous-node
  ir-node-prev @ ;

: next-node ( ir-node -- ir-node'|0 )
  ir-node-next @ ;

: link-nodes ( ir-node1 ir-node2 -- )
  2dup dup if ir-node-prev ! else 2drop then
  swap ir-node-next ! ;

: insert-node ( ir-node -- ir-node' )
  ir-node% %allocate throw
  over next-node over swap link-nodes
  tuck link-nodes ;

: mutate-node { ir-node type val -- ir-node }
  type ir-node ir-node-type  !
  val  ir-node ir-node-value !
  ir-node ;

: delete-node ( ir-node -- )
  dup previous-node over next-node link-nodes
  free throw ;

: .node ( ir-node -- )
  dup ir-node-value @
  swap ir-node-type @ case
    IR_NODE_CALL    of ." CALL "    hex. CR endof
    IR_NODE_LITERAL of ." LITERAL " hex. CR endof
    IR_NODE_BRANCH  of ." BRANCH "  hex. CR endof
    IR_NODE_RET     of ." RET"     drop  CR endof
    drop ." (unknown) " CR
  endcase ;


( IR Construction )

: make-ir ( -- ir )
  ir% %allocate throw
  0 over ir-entry% ! ;

: ir-entry ( ir -- ir-node )
  ir-entry% @ ;

: .ir ( ir -- )
  ir-entry
  cr
  begin ?dup while
    dup .node next-node
  repeat ;

: free-ir ( ir -- )
  dup ir-entry swap free throw
  begin ?dup while
    dup next-node swap free throw
  repeat ;
