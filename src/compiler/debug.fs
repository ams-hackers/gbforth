require ./ir.fs
require ./optimize.fs
require ./codegen.fs
require ./xname.fs

(code)
A inc,
A inc,
ret,
(end-code)
latestxt F_PRIMITIVE make-xname constant prim


make-ir
dup 0 make-xname constant test
insert-node IR_NODE_CALL ::type prim ::value
insert-node IR_NODE_CALL ::type prim ::value
insert-node IR_NODE_CALL ::type prim ::value
insert-node IR_NODE_RET  ::type
drop


make-ir
dup 0 make-xname constant foo
insert-node IR_NODE_CALL ::type test ::value
insert-node IR_NODE_RET  ::type
drop


." prim: " prim >xcode hex. CR CR
test >xcode .ir
foo  >xcode .ir


." --------------------" CR
foo >xcode gen-code
cr

