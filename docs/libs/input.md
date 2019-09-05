# input.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/input.fs)

The `input.fs` library contains words to read key input.

## Usage

Initialise the lib with `init-input` to set up the correct interrupt flags:

```forth
require input.fs

: main
  init-input ;
```

## Word Index

##### `key` _( -- c )_

Wait for a key press and return the keycode.

##### `key-state` _( -- c )_

Return the keycode of the currently pressed keys, or `0` when no keys are pressed.

## constants

Use these constants to determine which keycode corresponds to which key:

- `k-up`
- `k-down`
- `k-left`
- `k-right`
- `k-a`
- `k-b`
- `k-start`
- `k-start`
