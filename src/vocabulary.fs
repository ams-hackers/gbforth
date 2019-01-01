vocabulary gbforth
vocabulary gbforth-user

\
\ Expose words into the GBFORTH-USER vocabulary. Available within
\ [TARGET] in interpreting mode.
\
: [user-definitions]
  get-current
  also gbforth-user definitions ;

: [end-user-definitions]
  previous set-current ;

\
\ Expose words into the standard FORTH vocabulary. Available within
\ [HOST] in interpreting mode. Keep this list small!
\
: [host-definitions]
  get-current
  also Forth definitions ;

: [end-host-definitions]
  previous set-current ;
