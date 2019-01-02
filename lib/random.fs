\ Very basic xorshift RNG

\ Initialise the SEED variable to a non-zero value and
\ use RND or RANDOM to generate random numbers

: xorshift ( n -- n )
  dup 7 lshift xor
  dup 9 rshift xor
  dup 8 lshift xor ;

variable seed

: rnd ( -- n )
  seed @
  xorshift
  dup seed ! ;

\ WARNING: Not evenly distributed but should be good enough for small ranges
: random ( n -- n )
  rnd swap mod ;
