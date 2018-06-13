require ./ir.fs

( Node basic pattern maching )

: ` postpone postpone ; immediate

: (match-start) 0 ; immediate
: (match-end) 0 ?do postpone then loop ; immediate

: (match-open)
  >r ` dup ` 0<> ` if ` dup r> 1+
; immediate

: (match-close)
  >r ` if r> 1+
; immediate

: (match-true) ` drop ` true ` exit ; immediate
: (match-false) ` drop ` false ; immediate

: match<<
  ` (match-start)
  ` (match-open)
; immediate

: followed-by
  ` (match-close)
  ` next-node
  ` (match-open)
; immediate

: >>
  ` (match-close)
  ` (match-true)
  ` (match-end)
  ` (match-false)
; immediate


( Basic tail-call optimization [TCO]

  CALL xxx , RET

  can be transformed in BRANCH xxx
)

: call? ir-node-type @ IR_NODE_CALL = ;
: ret? ir-node-type @ IR_NODE_RET = ;

: tail-call? ( ir-node -- true|false )
  match<< call? followed-by ret? >> ;

: optimize-tail-call ( ir -- ir )
  dup ir-entry
  begin ?dup while
    dup tail-call? if
      dup next-node delete-node
      IR_NODE_BRANCH ::type
      over ir-node-value @ ::value
    then
    next-node
  repeat ;

: optimize-ir
  optimize-tail-call ;
