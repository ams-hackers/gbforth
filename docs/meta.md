# Meta Programming

_gbforth_ is a Forth cross-compiler. It runs on your computer while it will generate a program that will be executed on a Game Boy.

This imposes some restrictions compared to other Forth environments.

The most fundamental limitation is that you cannot _interpret_ code that is intended to be executed in the Game Boy. For instance,

```forth
: square dup * ;

\ This won't work, because we can't run SQUARE
: f [ 10 square ]L ;
```

The same restriction applies if you try to reference a colon-definition inside `[` ... `]`.

However, you are still able to get the _execution token_ (XT) of a word by using `'`

```forth
: square dup * ;
\ Leave the address of the word squareto the stack
' square
```

We can characterize code based on 2 criteria:

- **When** it is the code executed (compile-time vs run-time)
- **Where** it acts on (host system vs target system)

Note that it does not make sense for a computation in the target system to act on the host system, so we are left with 3 meaningful combinations.

## Colon definitions

> when: **run-time**, where: **target system**

This is the most common kind of code that you will write. It executes on the target and allows you to read/write variables, display to screen and all you would like to do with your device.

```forth
: bar
  %11100100 $FF47 ! ;
```

The words in the definition of `bar` would execute at run-time, storing a value to an address on the target.

gbforth does not provide a run-time Forth system, so for example, you **can't** create new words (`CREATE`), and you can't read words from the input stream (`PARSE`). You can, however, switch to interpreter mode and do those things at compile-time.

## Top-level code and interpretation

> when: **compile-time**, where: **target system**

In this mode, you can run words like `CREATE`, `CONSTANT`, `VARIABLE` and other defining and parsing Forth words.

```forth
CREATE foo 2 cells allot
```

In this code, `CREATE` is _executed_ during the compilation of the program. Likewise, `2 cells allot` will allocate 2 cells on the target system memory.

The word `:` switches to common definition / compilation mode as described above.

However, that introduces a challenge. Sometimes we want to build new abstractions on top of interpreting words. To do that, we can use the word `h:`, which allows us to create a word that can be executed on the _host_, but will still act on _target_ structures.

For example, we can define this _enumerations_ facility like:

```forth
h: begin-enum 0 ;
h: enum 1+ dup constant ;
h: end-enum drop ;
```

This code, unlike ordinary colon definitions, can be executed in interpreter mode, so we can use it like:

```forth
begin-enum
  enum DARKER
  enum DARK
  enum LIGHT
  enum LIGHTER
end-enum
```

This mode is intended to be as intuitive as possible, but it still imposes some important limitations. For example, you cannot allocate memory in the host system, as the words `allot`, `here` and so on act on the target data structures.

To use even more advanced scenarios, you will want to switch to _host definitions_.

## Host definitions

> when: **compile-time**, where: **host system**

Sometimes you'll want to write code that is _executed on the **host**_ system, and operates on _data in the **host**_ as well.

That is useful in advanced scenarios where, for example, you would like to allocate some memory for some compile-time computations, but that data does not need to be available at run-time at all.

### TODO:

- [ ] Write a good example
- [ ] Describe host definition - toplevel interop

## Summary

| Context              | Execution             | Data                 |
| -------------------- | --------------------- | -------------------- |
| Top-level            | Host _(compile-time)_ | Target _(ROM)_       |
| Colon definition `:` | Target _(run-time)_   | Target _(ROM / RAM)_ |
| `[host]`             | Host _(compile-time)_ | Host                 |
