\ run the MAIN word and BYE to stop the CPU
\ (just in case MAIN terminates)
\ this line force emits all the code
:noname main bye ;

\ mark the starting location of the cartridge
main:

\ initialise the runtime (SP/RP/CP)
[host]
also gbforth
runtime-init,
previous
[target]

[asm]
( noname XT ) # call,
[endasm]
