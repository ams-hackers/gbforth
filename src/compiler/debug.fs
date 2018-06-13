require ./ir.fs
require ./optimize.fs

make-ir constant test

test
insert-node IR_NODE_CALL 4242 mutate-node
insert-node IR_NODE_CALL 4242 mutate-node
insert-node IR_NODE_CALL 4242 mutate-node
insert-node IR_NODE_RET  0    mutate-node
drop

test
optimize-tail-call
.ir
