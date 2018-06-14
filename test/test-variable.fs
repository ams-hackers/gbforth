[asm]

variable foo
variable bar

: test
  $1337 foo !
  $4242 bar !
  foo @
  bar @ ;

main:
' test # call,

ret,

[endasm]
