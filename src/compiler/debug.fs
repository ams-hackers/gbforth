require ../user.fs
require ./cross.fs

also gbforth-user

require ../../lib/core.fs

: max
  dup dup < if nip else drop then ;

previous

xsee max

cr ." ---- CODE GENERATION -----" cr
x' max drop

xname' max >xcode free-ir
