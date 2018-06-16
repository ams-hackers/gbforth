require ../utils/memory.fs
require ../utils/misc.fs
require ./xname.fs


( Intermedian Representation [IR]

  As an intermediate representation for the compiler, we use a doubly
linked-list of ir-node% elements. This allows us to easily insert and
delete nodes.

  The CALL and BRANCH nodes refer to another XNAME.
)

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
  cell% field ir-addr
end-struct ir%

: previous-node
  ir-node-prev @ ;

: next-node ( ir-node -- ir-node'|0 )
  ir-node-next @ ;

: link-nodes ( ir-node1 ir-node2 -- )
  2dup dup if ir-node-prev ! else 2drop then
  swap ir-node-next ! ;

: insert-node ( ir-node -- ir-node' )
  ir-node% %zalloc
  over next-node over swap link-nodes
  tuck link-nodes ;

: ::type ( ir-node type -- ir-node )
  over ir-node-type ! ;

: ::value ( ir-node val -- ir-node )
  over ir-node-value ! ;

: delete-node ( ir-node -- )
  dup previous-node over next-node link-nodes
  free throw ;


: .node { ir-node -- }
  CR
  ir-node ir-node-value @
  ir-node ir-node-type @ case
    IR_NODE_LITERAL of ." LITERAL " hex. endof
    IR_NODE_RET     of ." RET"      drop endof
    IR_NODE_CALL    of ." CALL "   dup .xname ."  ( " hex. ." )" endof
    IR_NODE_BRANCH  of ." BRANCH " dup .xname ."  ( " hex. ." )" endof
    drop ." (unknown) "
  endcase ;


( IR Construction )

: make-ir ( -- ir )
  ir% %zalloc ;

: ir-entry ( ir -- ir-node )
  ir-entry% @ ;


( DO-NODES...END-NODES will iterate through all the nodes in the
  IR. At the end of each iteration, next-node will be called, so you
  are responsible to leave the 'current node' in the stack.

  This will let you do some node manipulations inside the body. 
 )
: do-nodes
  ` ir-entry
  ` begin ` ?dup ` while
; immediate

: end-nodes
  ` next-node
  ` repeat
; immediate

: .ir ( ir -- )
  do-nodes
    dup .node
  end-nodes ;

: free-ir-nodes
  do-nodes
    dup next-node
    >r free r>
  end-nodes ;
  
: free-ir ( ir -- )
  dup free-ir-nodes
  free ;
 
