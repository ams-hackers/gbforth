[asm]

' main

__start:

[host]
also gbforth
runtime-init,
previous
[endhost]

( main ) # call,

begin, halt, repeat,

[endasm]
