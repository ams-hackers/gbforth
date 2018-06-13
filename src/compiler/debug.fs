require ./ir.fs
require ./optimize.fs
require ./codegen.fs

(code)
A inc,
A inc,
ret,
(end-code)
latestxt constant prim


make-ir constant test

test
insert-node IR_NODE_CALL ::type prim ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_CALL ::type prim ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_CALL ::type prim ::value IR_FLAG_PRIMITIVE ::flags
insert-node IR_NODE_RET  ::type
drop


make-ir constant foo

foo
insert-node IR_NODE_CALL ::type test ::value 0 ::flags
insert-node IR_NODE_RET  ::type
drop


." prim: " prim hex. CR CR

test .ir
foo .ir

foo gen-code
cr
