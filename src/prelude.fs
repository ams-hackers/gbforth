\ get the XT of MAIN (this also force emits the code)
' main

\ mark the starting location of the cartridge
main:

\ initialise the runtime (SP/RP/CP)
[host]
also gbforth
runtime-init,
previous
[target]

[asm]
\ call the MAIN word
( main ) # call,

\ stop the CPU (just in case MAIN terminates)
begin, stop, again,
[endasm]
