[asm]

main:

( 4096/1024 = 4 => 4 )
#4096 push-lit,
#1024 push-lit,
' / # call,

( 4095/1024 = 3.99 => 3 )
#4095 push-lit,
#1024 push-lit,
' / # call,

( 4097/1024 = 4.001 => 4 )
#4097 push-lit,
#1024 push-lit,
' / # call,

begin, halt, repeat,

[endasm]
