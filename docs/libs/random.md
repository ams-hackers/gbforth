# random.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/random.fs)

The `random.fs` library contains very basic xorshift random number generator (RNG).

## Usage

Initialise the lib with by setting the `seed` variable to a non-zero value first.
This can be done in a couple of ways:

```forth
require random.fs

\ for some games using a constant value might be good enough
: main
  1234 seed ! ;

\ common pattern in Forth, but essentially a constant in gbforth
: main
  here seed ! ;

\ use the divider register (from gbhw.fs)
: main
  rDIV c@ 1 max seed ! ;
```

## Word Index

##### `random` _( n -- n )_

Generate a random number between 0 and _n_ (exclusive).

##### `rnd` _( -- n )_

Generate a random number.

##### `shuffle` _( a-addr u -- )_

Shuffles _u_ cells starting from _a-addr_.

##### `cshuffle` _( c-addr u -- )_

Shuffles _u_ chars starting from _c-addr_.
