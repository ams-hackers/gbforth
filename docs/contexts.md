# Contexts

Since gbforth is a cross-compiler, and the host and target system differ a lot
from each other, it's important to understand in which context your code is
running.

You can distinguish 4 contexts in which you can write code:

- [Toplevel](#toplevel) (default)
- [Target Definition](#target-definition)
- [Meta Definition](#meta-definition)
- [Host Execution](#host-execution)

Although the **toplevel** context is the default, most of your code will be
written in **target definitions** (this is the code that will actually run on
the target).

If you are confused about words with the same name appearing in multiple
contexts, refer to the [Cross-context](#cross-context) section.

## Toplevel

Executed on host while compiling the program, but referencing the target memory.
This is the default context, accessed with `[target]`.

```
[rom]
$1 c,

\ Writes byte `1` to target ROM
```

_Note: The default memory space is `[ram]`, but is unavailable for writing at
compile-time._

## Target Definition

Defined and executed on target. These are the default colon definitions, and can
only be executed at run time.

Switching to "interpreter mode" with `[` will take you to **toplevel**, and `]`
returns to the **target definition**.

```
: main
  $2 c, ;

\ Writes byte `2` to RAM
```

_Note: The `main` word is executed automatically when running the program._

## Meta Definition

Defined and executed on host. Basically colon definitions for **toplevel**.

```
:m bar
  [rom] $3 c, ;

bar

\ Writes `3` to target ROM
```

## Host Execution

Defined and executed on host, but unlike **toplevel** is referencing the host
memory.

```
[host] $4 c, [target]

\ Writes byte `4` to host memory
\ not affecting the target in any way
```

_Note: You can switch to the `[host]` context both at **toplevel** and in
**meta definitions**._

## Cross-context

Some build-in Forth words from the **Host Execution** context are copied to the
**Toplevel** and **Meta Definition** context. Examples are stack operations
(`dup`, `swap`, `drop`), arithmetic/logic operators (`+`, `>`, `xor`) and words
that might be useful for debugging (`quit`, `.s`, `words`).

This is done for convenience, so you don't have to manually switch context for
common words:

```
: show-a
  [ 90 7 [host] + [target] ]L
  emit ;

: show-a
  [ 90 7 + ]L emit ;
```

However, some words that seem to be available in multiple contexts, might have
a different definition. The word `c,` is a good example for this:

- **Host Execution**: The build-in Forth word definition that writes a byte to the host memory.
- **Toplevel** and **Meta Definition**: Redefined to output a byte to the target ROM.
- **Target Definition**: Implemented at the target to write a byte to the RAM.

Another example that could be confusing (but convenient in most cases) is the
`s"` word:

- **Host Execution**: The build-in Forth word definition that parses a string delimited by `"` and returns the `(addr u)` of where the string is stored in your host memory.
- **Toplevel** and **Meta Definition**: Redefined to parse the string as before, then append it to the target ROM, and return the `(addr u)` in the ROM.
- **Target Definition**: Parses and appends the string to the ROM at compile-time, and returning the `(addr u)` at run-time.
