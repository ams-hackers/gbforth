require ../utils/memory.fs
require ../utils/misc.fs
require ../set.fs
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
\ FORK and CONTINUE nodes allow us to represent control flow. See the
\ code for the immediate words `if`, `else` and `then` for further
\ information.
5 constant IR_NODE_CONTINUE
6 constant IR_NODE_FORK


struct
\ The ir-node-next field must be the first field of this struct, as it
\ is shared by the ir% struct.
  cell% field ir-node-next
  cell% field ir-node-prev
  cell% field ir-node-type
  cell% field ir-node-value
  cell% field ir-node-value'    ( secondary value )
end-struct ir-node%

' ir-node-value  alias ir-fork-consequent
' ir-node-value' alias ir-fork-alternative

struct
\ The ir-entry field must be the first field of this struct, as it is
\ shared by the ir-node% struct. In that sense, we use the ir% struct
\ as a sentinel node.
  cell% field ir-entry%
  cell% field ir-addr
  cell% field ir-visited
end-struct ir%

: previous-node
  ir-node-prev @ ;

: next-node ( ir-node -- ir-node'|0 )
  ir-node-next @ ;

: last-node ( ir-node -- ir-node' )
  begin dup next-node dup while nip repeat drop ;

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

: ::value' ( ir-node val -- ir-node )
  over ir-node-value' ! ;

: delete-node ( ir-node -- )
  dup previous-node over next-node link-nodes
  free throw ;

: .node { ir-node -- }
  CR
  ir-node ir-node-value @
  ir-node ir-node-type @ case
    IR_NODE_LITERAL  of ." LITERAL " hex. endof
    IR_NODE_RET      of ." RET"      drop endof

    IR_NODE_CALL     of ." CALL "   dup .xname ."  ( " hex. ." )" endof
    IR_NODE_BRANCH   of ." BRANCH " dup .xname ."  ( " hex. ." )" endof

    IR_NODE_CONTINUE of ." CONTINUE " hex. endof

    IR_NODE_FORK of
      drop
      ." 0<>IF "
      ir-node ir-fork-consequent @ hex.
      ." ELSE "
      ir-node ir-fork-alternative @ hex.
    endof

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
  ` repeat
; immediate

: next-ir-components ( ir -- ir1|0 ir2|0 )
  last-node 
  dup ir-node-type @ case
    IR_NODE_CONTINUE of
      ir-node-value @ 0
    endof
    IR_NODE_FORK of
      dup  ir-fork-alternative @
      swap ir-fork-consequent  @
    endof
    nip 0 0 rot
  endcase
;

: print-ir-components recursive { ir visited -- }
  ir visited in? if exit then
  ir visited add-to-set

  cr cr ." [ " ir hex. ." ]"
  ir do-nodes dup .node next-node end-nodes

  ir next-ir-components
  ?dup if visited print-ir-components then
  ?dup if visited print-ir-components then
;

: .ir ( ir -- )
  make-set tuck print-ir-components free-set ;


( Freeing IR memory )

: free-ir-nodes ( ir -- )
  do-nodes
    dup next-node >r
    free throw
    r>
  end-nodes ;

: free-ir-components recursive { ir visited -- }
  ir visited in? if exit then
  ir visited add-to-set

  ir next-ir-components
  ?dup if visited free-ir-components then
  ?dup if visited free-ir-components then

  ir free-ir-nodes
  ir free throw ;
  
: free-ir
  make-set tuck free-ir-components free-set ;
