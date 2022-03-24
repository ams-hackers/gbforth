require ../rom.fs
require ../sym.fs
require ../utils/memory.fs
require ../utils/errors.fs

require ./ir.fs
require ./code.fs
require ./codegen.fs

require ./xname.fs

( Constants )
: xconstant ( n -- )
  F_CONSTANT create-xname ;

( Cross Compiler )

\ Read the next word available in the inputs stream. Automatically
\ refill the stream if needed.
: parse-next-name
  parse-name dup if
  else
    refill 0= throw
    2drop recurse
  then ;

( 0 if we the host is interpreting words,
 -1 if we are compiling into the target )
variable xstate

-1 value current-node

: xcompiling?
  current-node -1 <>
  xstate @ 1 = and ;

: xinterpreting?
  current-node -1 <>
  xstate @ 0 = and ;

make-ir constant unreachable-node

: unreachable?
  current-node unreachable-node = ;

: xliteral, { n -- }
  unreachable? if
    s" Unreachable code" .warn-line
  then
  current-node
  insert-node IR_NODE_LITERAL ::type n ::value
  to current-node ;

: xcompile, { xname -- }
  unreachable? if
    s" Unreachable code" .warn-line
  then
  current-node
  insert-node IR_NODE_CALL ::type xname ::value
  to current-node ;

: xreturn,
  unreachable? invert if
    current-node
    insert-node IR_NODE_RET ::type
    to current-node
  then ;

: process-xname ( xname -- )
  dup ximmediate? if
    >xcode execute
  else
    dup xconstant? if
      >xcode xliteral,
    else
      xcompile,
    then
  then ;

: process-token ( addr u -- )
  2dup find-xname ?dup if
    nip nip process-xname
  else
    s>number? if
      drop xliteral,
    else
      -1 abort" Unknown word"
    then
  then ;


: x[ 0 xstate ! ; ximmediate-as [

: xliteral xliteral, ; ximmediate-as literal

: x\ postpone \ ; ximmediate-as \
: x( postpone ( ; ximmediate-as (
\ The next parenthesis is only here to make the editor happy!
)

\
\ Note that X] will return when the cross-compilation is finished (
\ with X[ ), unlike other Forth systems.
\
\ In normal Forth systems, this would switch to compiling mode, so the
\ outer interpreter the responsible to keep iterating and compiling
\ words from the input source.
\
\ Unfortunately we can't use the GForth outer interpreter that way,
\ because we can't customize how numbers will be processed. We would
\ like to cross-compiling them, not pushing them to the stack.
\
\ Instead, this implements its own reading from the host input stream
\ and it will return when the cross-compiling mode is finished.
\
: x]
  1 xstate !
  begin
    parse-next-name
    process-token
    xstate @ while
  repeat ;

: xname'
  parse-next-name find-xname
  dup 0= if
    -1 abort" Unknown word"
  then ;

: xpostpone, ( xname -- )
  postpone literal
  postpone process-xname ;

: xsee-xname ( xname -- )
  cr
  ." ========== " dup .xname ." ( " dup hex. ." ) " ." ========== "
  dup xprimitive? if
    cr ." (code)" drop
  else
    >xcode .ir
  then
  cr ;

: xsee
  xname' xsee-xname ;

: x'
  xname' xname>addr ;

: x['] x' xliteral, ; ximmediate-as [']

: x'body xname' >xdfa xliteral, ;

create user-name 128 chars allot

( Copy a string into colon-name to persist it! )
: copy-user-name ( addr u -- addr' u )
  dup 128 >= abort" Name is too large!"
  dup >r user-name swap move
  user-name r> ;

: parse-user-name
  parse-next-name copy-user-name ;


\ Defining words
\
\ Note that those words will return when the full definition has been
\ processed, unlike in ANS Forth. See X] for further information.
\

0 constant WORD_NONAME
1 constant WORD_NAMED
2 constant WORD_DOES

0 0 2constant unnamed

\ The output of this word depends on the TYPE.
: create-word ( type c-addr u -- ...? )
  make-ir to current-node
  current-node
  x] ;

: finalize-word { type c-addr u ir }
  -1 to current-node
  ir finalize-ir
  type WORD_DOES = if
    ir
  else
    c-addr u nextname
    ir 0 create-xname
    type WORD_NONAME = if
      xlatest xname>addr
    then
  then
;

: x:noname ( -- addr )
  WORD_NONAME
  unnamed
  create-word ;

: x:
  WORD_NAMED
  parse-user-name
  create-word ;

: xalias ( addr -- )
  ( addr ) make-ir tuck ir-addr !
  parse-user-name nextname ( ir ) 0 create-xname ;

: xdoes:
  WORD_DOES
  unnamed
  create-word ;

: x; ( type c-addr u ir -- )
  xreturn,
  x[
  finalize-word ;


( Control Flow )

\ Create a unresolved mark.
: @there ( -- mark )
  make-ir ;

\ Compile a jump to a mark. Code compiled directly afte rthe mark
\ becomes unreachable.
: branch, { mark -- }
  current-node
  insert-node IR_NODE_CONTINUE ::type mark ::value
  drop
  unreachable-node to current-node ;

\ Compile a jump to a mark if the top of the stack is zero.
: 0branch, { mark -- }
  make-ir { continue }
  current-node
  insert-node IR_NODE_FORK ::type continue ::value mark ::value'
  drop
  continue to current-node ;

\ Resolve a mark to the current location.
: @resolve ( mark -- )
  dup branch, to current-node ;



( Code definitions )

[asm]

: code
  parse-user-name
  (code) ;

: -end-code
  postpone (end-code)
  >r nextname
  r> F_PRIMITIVE create-xname
; immediate

: end-code
  postpone ret,
  postpone -end-code
; immediate

[endasm]
