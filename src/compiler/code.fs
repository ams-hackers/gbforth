require ../rom.fs
require ../asm.fs

: memoize-code ( xt -- )
  noname create -1 , ,
  does>
  dup @ -1 <> if
    \ The code emition has already been executed, then we can just
    \ leave the value on the stack
    @
  else
    rom-offset over !
    rom-offset swap cell+ @ execute
  then ;

: (code)
  [asm]
  noname : ;

: (end-code)
  postpone ;
  [endasm]
  latestxt memoize-code
  latestxt
; immediate

: emit-code
  execute ;

: emitted-code? ( xt -- flag )
  >body @ -1 <> ;

