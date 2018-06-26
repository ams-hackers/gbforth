( Cross words )
require ../utils/memory.fs

( Cross Dictionary )
wordlist constant xwordlist
-1 value xlatest


%001 constant F_IMMEDIATE
%010 constant F_CONSTANT
%100 constant F_PRIMITIVE

struct
  cell% field xname-flags
  cell% field xname-addr
end-struct xname%

: allot-xname { addr flag -- xname }
  xname% %zallot
  addr over xname-addr !
  flag over xname-flags ! ;

: create-xname ( addr flag -- )
  get-current >r
  xwordlist set-current
  allot-xname create ,
  latestxt is xlatest
  r> set-current ;

: xname>string ( xname -- u-addr n )
  >name ?dup if name>string
  else 0 0 then ;

: >xcode >body @ xname-addr @ ;
: >xflags >body @ xname-flags @ ;

: ximmediate? >xflags F_IMMEDIATE and 0<> ;
: xconstant?  >xflags F_CONSTANT  and 0<> ;
: xprimitive? >xflags F_PRIMITIVE and 0<> ;

: ximmediate-as
  latestxt F_IMMEDIATE create-xname ;

: ximmediate
  latest name>string nextname
  ximmediate-as ;

: find-xname ( addr u -- xname )
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
