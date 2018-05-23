( Add lib/ to the search path of the game )
s" DMGFORTH_PATH" getenv 2constant dmgforth-path

: usage
  ." Usage: dmgforth [--no-kernel] <input> <output>" cr
  1 (bye) ;

1 arg s" --no-kernel" compare 0= [if]
  true Value --no-kernel
  shift-args
[else]
  false Value --no-kernel
[then]

argc @ 3 <> [if]
  ' usage stderr outfile-execute
[then]

next-arg 2constant input-file
next-arg 2constant output-file
