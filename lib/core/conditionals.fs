:m if ( -- alternative )
  @there dup 0branch,
; immediate

:m else ( alternative -- continuation )
  @there dup branch,
  swap @resolve
; immediate

:m then ( continuation -- )
  @resolve
; immediate

:m ahead ( -- orig )
  @there dup branch,
; immediate

:m endif postpone then ; immediate
