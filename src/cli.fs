( Add lib/ to the search path of the game )
s" GBFORTH_PATH" getenv 2constant gbforth-path

: process-exit
  1 (bye) ;

: usage
  ." Usage: gbforth [--no-kernel] <input> <output>" cr ;

false Value --no-kernel
false Value --verbose
false Value --debug

: arg= 1 arg compare 0= ;

: process-option
  s" --no-kernel" arg= if
    true To --no-kernel
    shift-args
    true exit
  then 

  s" --verbose" arg= if
    true To --verbose
    shift-args
    true exit
  then

  s" --debug" arg= if
    true To --debug
    shift-args
    true exit
  then

  s" --help" arg= if
    usage
    process-exit
    true exit
  then
  
  false 
;

[begin] process-option [while] [repeat]

argc @ 3 <> [if]
  ' usage stderr outfile-execute
  process-exit
[then]

next-arg 2constant input-file
next-arg 2constant output-file
