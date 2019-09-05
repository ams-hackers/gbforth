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

  postpone 2r@
  postpone <>
  postpone if

  postpone begin
  @there leave-push
; immediate

:m do
  postpone 2>r
  0 \ enter inconditionally!
  postpone begin
  @there leave-push
; immediate

:m I
  postpone r@
; immediate

:m J
  postpone r>
  postpone r>
  postpone r@
  postpone -rot
  postpone >r
  postpone >r
; immediate

:m K
  postpone r>
  postpone r>
  postpone r>
  postpone r>
  postpone r@
  postpone -rot
  postpone >r
  postpone >r
  postpone -rot
  postpone >r
  postpone >r
; immediate

:m unloop
  postpone 2rdrop
; immediate

:m leave
  leave-pop
  dup branch,
  leave-push
; immediate

: different-sign? ( n1 n2 -- f )
  $8000 and swap
  $8000 and xor ;

\ The standard mandates that the loop will finish when the index
\ crosses the limit between N-1 and N. Equivalently, to say, I-N and
\ I+u-N must have different sign bit
: +loop-index-next ( u I N -- f I+u )

  >r                ( u I           R: N )
  tuck + tuck       ( I+u I I+u     R: N )
  swap r@ -
  swap r> -         ( I+u I-N I+u-N )
  different-sign?   ( I+u f -- )
  swap ;


:m +loop ( u -- )
  postpone r>
  postpone r@
  postpone +loop-index-next ( finished? I+u )
  postpone >r
  postpone until

  [host] ?dup if [target]
    postpone then
  [host] then [target]

  leave-pop @resolve
  postpone unloop
; immediate

:m loop
  1 postpone literal
  postpone +loop
; immediate
