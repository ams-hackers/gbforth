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

##### `.` *( n -- )*
Prints the number to the screen and moves the cursor position.

##### `.r` *( n1 n2 -- )*
Prints the number *n1* to the screen, using at least *n2* positions.

##### `."`
Reads the string up to the first `"` character, and prints the string to the
screen.

##### `?` *( addr -- )*
Prints the value at *addr* to the screen.

##### `at-xy` *( x y -- )*
Sets the cursor position to the given coordinates.

##### `cr` *( -- )*
Prints a newline (moves the cursor down).

##### `emit` *( c -- )*
Prints the character with the given character code.

##### `page` *( -- )*
Clears the entire screen.

##### `space` *( -- )*
Prints a single space to the screen.

##### `spaces` *( n -- )*
Prints *n* spaces to the screen.

##### `type` *( addr c -- )*
Prints the string starting from *addr* with length *c* to the screen.

##### `typewhite` *( addr c -- )*
Prints *c* spaces to the screen.
