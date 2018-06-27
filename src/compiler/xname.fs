( Cross words )
require ../utils/memory.fs
require ./xmemory.fs

( Cross Dictionary )
wordlist constant xwordlist
-1 value xlatest


%001 constant F_IMMEDIATE
%010 constant F_CONSTANT
%100 constant F_PRIMITIVE

struct
  cell% field xname-flags
  \ Execution semantics for this word. If the XNAME is PRIMITIVE, then
  \ it references a code definition, which can be processed with words
  \ like `EMIT-CODE`. Otherwise, this is the root IR of a colon
  \ definition.
  cell% field xname-code
  \ The XT of a linked host word. The cross-word CREATE will define
  \ both this xname and a host word. This is the XT of the host word.
  cell% field xname-host-xt
  \ Data field address. This is initialized to the position in ROM/RAM
  \ at the time the word was defined, as returned by `xHERE`. This
  \ value is pushed to the stack by the initialization code compiled
  \ by DOES>.
  cell% field xname-dfa
end-struct xname%


: allot-xname { code flag -- xname }
  xname% %zallot
  code  over xname-code !
  flag  over xname-flags !
  xhere over xname-dfa ! ;

: create-xname ( code flag -- )
  get-current >r
  xwordlist set-current
  allot-xname create ,
  latestxt is xlatest
  r> set-current ;

: xname>string ( xname -- c-addr n )
  >name ?dup if name>string
  else 0 0 then ;

: >xcode >body @ xname-code @ ;
: >xflags >body @ xname-flags @ ;
: >xhost >body @ xname-host-xt @ ;
: >xhost! >body @ xname-host-xt ! ;

: ximmediate? >xflags F_IMMEDIATE and 0<> ;
: xconstant?  >xflags F_CONSTANT  and 0<> ;
: xprimitive? >xflags F_PRIMITIVE and 0<> ;

: ximmediate-as
  latestxt F_IMMEDIATE create-xname ;

: ximmediate
  latest name>string nextname
  ximmediate-as ;

: find-xname ( c-addr u -- xname )
  2>r
  get-order
  xwordlist 1 set-order
  2r>

  find-name dup if name>int then

  >r
  set-order
  r> ;

\ for debugging
: xwords
  xwordlist >order words previous ;

: .xname
  >name ?dup if id. then ;
