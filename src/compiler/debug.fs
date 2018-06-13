require ./ir.fs
require ./optimize.fs

make-ir constant test

test
insert-node IR_NODE_CALL ::type 4242 ::value
insert-node IR_NODE_CALL ::type 4242 ::value
insert-node IR_NODE_CALL ::type 4242 ::value
insert-node IR_NODE_RET  ::type
drop

test
optimize-tail-call
.ir
