# term.fs (lib)

The `term.fs` library helps you emulate a terminal on the screen.

## Usage

Use `init-term` to initialise this library. In order to be able to write
anything to the screen, you need to install a font as a tileset first. You
can use [ibm-font.fs](./ibm-font.md) for this.

```forth
require ibm-font.fs
require term.fs

: main
  install-font
  init-term ;
```

## Word Index

##### `.` _( n -- )_

Prints the number to the screen and moves the cursor position.

##### `.r` _( n1 n2 -- )_

Prints the number _n1_ to the screen, using at least _n2_ positions.

##### `."`

Reads the string up to the first `"` character, and prints the string to the
screen.

##### `?` _( addr -- )_

Prints the value at _addr_ to the screen.

##### `at-xy` _( x y -- )_

Sets the cursor position to the given coordinates.

##### `cr` _( -- )_

Prints a newline (moves the cursor down).

##### `emit` _( c -- )_

Prints the character with the given character code.

##### `page` _( -- )_

Clears the entire screen.

##### `space` _( -- )_

Prints a single space to the screen.

##### `spaces` _( n -- )_

Prints _n_ spaces to the screen.

##### `type` _( addr c -- )_

Prints the string starting from _addr_ with length _c_ to the screen.

##### `typewhite` _( addr c -- )_

Prints _c_ spaces to the screen.
