# Meta Programming

_gbforth_ is a Forth cross-compiler. It runs on your computer (the _host system_) while it will generate a program that will be executed on a Game Boy (the _target system_).

This imposes some restrictions compared to other Forth environments.

The most fundamental limitation is that you cannot _interpret_ code that is intended to be executed on the target. For instance, the following code will not work:

```forth
: square dup * ;

\ We can't run SQUARE on the host!
: f [ 10 square ]L ;
```

However, you are still able to get the _execution token_ (XT) of a word by using `'` as follows:

```forth
: square dup * ;

\ Push the target address of SQUARE to the stack
' square
```

We can characterize code based on 2 criteria:

- **When** the code is executed (_compile-time_ or _run-time_)
- **Where** it acts on (_host system_ or _target system_)

Since it does not make sense for a computation in the target system to act on the host system, we are left with 3 meaningful combinations:

  - **Colon definitions** execute at _run-time_, act on the _target_ system
  - **Top-level code** executes at _compile-time_, acts on the _target_ system
  - **Host definitions** execute at _compile-time_, act on the _host_ system

## Colon definitions

> when: **run-time**, where: **target system**

This is the most common kind of code that you will write. It executes on the target and allows you to read/write variables, display to screen and all you would like to do with your device.

```forth
: foo
  %11100100 $FF47 ! ;
```

The words in the definition of `foo` would execute at _run-time_, storing a value to an address on the _target_.

gbforth does not provide a run-time Forth system, so for example, you **can't** create new words (`CREATE`), and you can't read words from the input stream (`PARSE`). You can, however, switch to interpreter mode (using `[` and `]`) and do those things at compile-time.

## Top-level code and interpretation

> when: **compile-time**, where: **target system**

In this mode, you can run words like `CREATE`, `CONSTANT`, `VARIABLE` and other defining and parsing Forth words.

```forth
CREATE bar 2 cells allot
```

In this code, `CREATE` is _executed_ during the compilation of the program. Likewise, `2 cells allot` will allocate 2 cells on the target system memory.

### Top-level colon definitions

As described above, the word `:` switches to colon definition / compilation mode for code that is meant to be executed on the _target_ system.

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

Sometimes you'll want to write code that is executed on the host system, and operates on data in the host as well.

That is useful in advanced scenarios where, for example, you would like to allocate some memory for some compile-time computations, but that data does not need to be available at run-time at all.

### TODO:

- [ ] Write a good example
- [ ] Describe host definition - toplevel interop

## Summary

| Context              | Execution             | Data                 |
| -------------------- | --------------------- | -------------------- |
| Colon definition `:` | Run-time _(target)_   | Target _(ROM / RAM)_ |
| Top-level            | Compile-time _(host)_ | Target _(ROM)_       |
| `[host]`             | Compile-time _(host)_ | Host                 |
