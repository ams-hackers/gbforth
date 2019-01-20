# input.fs (lib)

The `input.fs` library contains words to read key input.

## Usage

Initialise the lib with `init-input` to set up the correct interrupt flags:

```forth
require input.fs

: main
  init-input ;
```

## Word Index

##### `key` *( -- n )*
Wait for a key press and return the keycode.

##### `key-state` *( -- n )*
Return the keycode of the currently pressed key, or `0` when no key is pressed.

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
