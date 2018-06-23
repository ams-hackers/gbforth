:m case 0 ; immediate

:m of
  1+ >r
  postpone over
  postpone =
  postpone if
  postpone drop
  r>
; immediate

:m endof
  >r postpone else r>
; immediate

:m endcase
  postpone drop
  0
  [host] ?do [target]
    postpone then
  [host] loop [target]
; immediate
