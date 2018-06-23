[host]

\ The leave stack provides support for the LEAVE word. The following
\ two examples illustrate why we need an auxiliary stack:
\
\ : ex1
\   10 0 ?do leave loop ;
\
\ : ex2
\   10 0 ?do i 3 = if leave then loop ;
\
\ Because LEAVE can appear inside other constructions, LEAVE can't
\ know where in the control stack (in the data stack for us) the
\ destination of the jump will be
\
create leave-stack 10 cells allot
variable leave-count 0 leave-sp !

: check-leave-stack-integrity
  leave-count @ 0 10 within invert abort" The leave stack pointer is out of range" ;

: leave-pointer ( -- addr )
  check-leave-stack-integrity
  leave-stack leave-count @ cells + ;

: leave-push ( mark -- )
  leave-pointer !
  1 leave-count +! ;

: leave-pop ( -- mark )
  -1 leave-count +!
  leave-pointer @ ;

[target]


:m ?do
  postpone 2>r
  postpone ahead
  postpone begin
  swap
  @there leave-push
; immediate

:m do
  postpone 2>r
  postpone begin
  0 \ not ahead!
  @there leave-push
; immediate

:m i
  postpone r@
; immediate

:m unloop
  postpone 2rdrop
; immediate

:m leave
  leave-pop
  dup branch,
  leave-push
; immediate

:m loop
  postpone r>
  postpone 1+
  postpone >r

  [host] ?dup if [target]
    postpone then
  [host] then [target]

  postpone 2r@
  postpone <>
  postpone while

  postpone repeat
  leave-pop @resolve
  postpone unloop
; immediate
