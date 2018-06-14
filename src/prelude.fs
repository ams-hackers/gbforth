[asm]

' main

__start:

[host]
also dmgforth
runtime-init,
previous
[endhost]

( main ) # call,

begin, halt, repeat,

[endasm]
