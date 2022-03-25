# assert.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/assert.fs)

The `assert.fs` library contains words to add assertions to your code.

## Usage

Simply write assertions in your code like this:

```forth
require assert.fs
1 assert-level! \ this is the default

: main
  assert( depth 0= )
  \ other code goes here...
  assert( 1 2 + 3 = ) ;
```

The code after the word `assert(` should push a flag to the parameter stack that
indicates whether the assertion succeeded. The word `)` consumes this flag and
displays an error on `false`.

Make sure to not modify the stack in any other way between the `assert(` and `)`
words.

## Assertion levels

The default `assert-level` is set to `1`, which means that assertions defined
with `assert0(` and `assert1(` are enabled. Other assertions (at level 2 and 3)
will be skipped at compile-time and are not included in your final ROM.

The assert level can be set by calling `assert-level!` at toplevel. This value
can not be modified at run-time.

## Word Index

##### `assert-level!` _( u -- )_

Set the assertion level. Valid levels are `0`, `1`, `2`, `3` (default is `1`).
Toplevel only, can not be used at run-time.

##### `assert(` _( -- )_

Alias for `assert1(`, the default assert.

##### `assert0(` _( -- )_

For important assertions that are always enabled.

##### `assert1(` _( -- )_

For regular assertions that are enabled by default.

##### `assert2(` _( -- )_

For debugging assertions that can be enabled manually.

##### `assert3(` _( -- )_

For extra slow assertions that can be enabled for thorough debugging.

##### `)` _( f -- )_

Ends the assertion. This word is doing the actual checking at run-time.
