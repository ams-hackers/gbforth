# term.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/term.fs)

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

##### `?` _( c-addr -- )_

Prints the value at _c-addr_ to the screen.

##### `at-xy` _( u1 u2 -- )_

Moves the cursor to column _u1_ and row _u2_.

##### `cr` _( -- )_

Prints a newline (moves the cursor down).

##### `emit` _( c -- )_

Prints the character with the given character code.

##### `page` _( -- )_

Clears the entire screen.

##### `space` _( -- )_

Prints a single space to the screen.

##### `spaces` _( u -- )_

Prints _u_ spaces to the screen.

##### `type` _( c-addr u -- )_

Prints the string starting from _c-addr_ with length _u_ to the screen.

##### `typewhite` _( c-addr u -- )_

Prints _u_ spaces to the screen.
