# stack.fs (lib)

The `stack.fs` library contains words define and use stacks.

## Usage

Define a new stack with `STACK` and `clear` it before use.

```forth
require stack.fs

10 STACK my-stack

: main
  my-stack clear ;
```

## Word Index

##### `clear` _( a-addr -- )_

Clears the stack.

##### `empty?` _( a-addr -- f )_

Returns whether the stack is empty.

##### `peek` _( a-addr -- x )_

Returns the top value from the stack, without removing it.

##### `pop` _( a-addr -- x )_

Pops the top value from the stack.

##### `push` _( x a-addr -- )_

Pushes a value on the stack.

##### `size` _( a-addr -- u )_

Returns the number of values on the stack.

##### `STACK` _( u "name" -- )_

Create a new stack for at most _u_ values.
