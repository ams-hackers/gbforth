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

: make-xname ( addr flag -- xname )
  >r >r
  xname% %zallot
  r> over xname-addr !
  r> over xname-flags ! ;

: >xcode xname-addr @ ;
: >xflags xname-flags @ ;
: ximmediate? >xflags F_IMMEDIATE and 0<> ;
: xconstant?  >xflags F_CONSTANT  and 0<> ;
: xprimitive? >xflags F_PRIMITIVE and 0<> ;


: create-xname ( addr flag -- )
  get-current >r
  xwordlist set-current
  make-xname
  dup IS xlatest
  create ,
  r> set-current ;

: ximmediate-as
  latest name>int F_IMMEDIATE create-xname ;

: find-xname ( addr u -- xname )
  2>r
  get-order
  xwordlist 1 set-order
  2r>

  find-name dup if name>int >body @ then

  >r
  set-order
  r> ;

\ for debugging
: xwords
  xwordlist >order words previous ;

