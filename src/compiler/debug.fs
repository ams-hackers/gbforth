require ./ir.fs
require ./optimize.fs
require ./codegen.fs

make-ir constant test

test
insert-node IR_NODE_CALL ::type 4242 ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_CALL ::type 4242 ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_CALL ::type 4242 ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_RET  ::type
drop


make-ir constant foo

foo
insert-node IR_NODE_CALL ::type test ::value 0 ::flags
insert-node IR_NODE_RET  ::type
drop

foo  gen-code
\ foo .ir
