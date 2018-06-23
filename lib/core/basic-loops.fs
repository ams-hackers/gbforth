:m begin ( -- dest )
  @there dup @resolve
; immediate

:m while ( dest -- orig dest )
  @there dup 0branch, swap
; immediate

:m repeat ( orig dest -- )
  branch,
  @resolve
; immediate

:m again ( dest -- )
  branch,
; immediate

:m until ( dest -- )
  0branch,
; immediate

