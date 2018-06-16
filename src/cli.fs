( Add lib/ to the search path of the game )
s" GBFORTH_PATH" getenv 2constant gbforth-path

: process-exit (bye) ;

: usage
  ." Usage: gbforth [<options>] input.fs output.gb" CR
  CR
  ." Options:" CR
  ."   -h, --help       print usage information" CR
  ."   -v, --verbose    enable verbose output" CR
  ."   -d, --debug      enable debugging mode" CR
  ."   --no-kernel      exclude the kernel, only use assembler" CR
  CR ;

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

  s" -v" arg=
  s" --verbose" arg= or if
    true To --verbose
    shift-args
    true exit
  then

  s" -d" arg=
  s" --debug" arg= or if
    true To --debug
    shift-args
    true exit
  then

  s" -h" arg=
  s" --help" arg= or if
    usage
    0 process-exit
    true exit
  then

  false
;

[begin] process-option [while] [repeat]

argc @ 3 <> [if]
  ' usage stderr outfile-execute
  1 process-exit
[then]

next-arg 2constant input-file
next-arg 2constant output-file
