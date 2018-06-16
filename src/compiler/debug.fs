require ../user.fs
require ./cross.fs

also gbforth-user

code prim
  A inc,
  A inc,
end-code

: test prim prim prim ;
: foo test ;

previous

xsee prim
xsee test
xsee foo

x' foo ( emit code )
cr
