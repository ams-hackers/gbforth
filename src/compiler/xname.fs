( Cross words )

%001 constant F_IMMEDIATE
%010 constant F_CONSTANT
%100 constant F_PRIMITIVE

struct
  cell% field xname-flags
  cell% field xname-addr
end-struct xname%

: make-xname ( addr flag -- xname )
  >r >r
  xname% %zalloc
  r> over xname-addr !
  r> over xname-flags ! ;

: >xcode xname-addr @ ;
: >xflags xname-flags @ ;
: ximmediate? >xflags F_IMMEDIATE and 0<> ;
: xconstant?  >xflags F_CONSTANT  and 0<> ;
: xprimitive? >xflags F_PRIMITIVE and 0<> ;
