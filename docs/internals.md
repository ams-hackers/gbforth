gbForth's internals
===================

gbforth includes a Forth cross-compiler. The cross-compiler is missing
many optimizations and functionality but it is quite easy to work
with. This document gives an overview of how to improve it.

## Compiler

The entrypoint of the gbforth compiler can be found at
`src/compiler/cross.fs`

This file implements the equivalent of the "outer interpreter" in
Forth. In particular, the word `x]` (cross-version of `]`), will
switch to cross-compiling mode, reading words and cross-compiling
them. Note that this does not reuse the host Forth outer interpreter.

gbforth compiles words into an [intermediate representation
(IR)](https://github.com/ams-hackers/gbforth/blob/master/src/compiler/ir.fs). This
represents the code as a graph of IR components, each of them being a
sequence of IR nodes (e.g: CALL node to call another word),
corresponding to the traditional basic blocks in compiler terminology.

This intermediate representation are saved together with cross-words,
as defined in
[xname.fs](https://github.com/ams-hackers/gbforth/blob/master/src/compiler/xname.fs). However,
the code is not actually compiled into assembler in the ROM just yet.

On the intermediate representation, some optimizations can take
place. There are no optimizations in place yet, but you can see an
example of how tail-call [optimization could be
implemented](https://github.com/ams-hackers/gbforth/blob/master/src/compiler/optimize.fs).

Once the whole game source code has been processed, we have a
collection of xnames, linked to IR or code primitives. In order to
produce the ROM, the [code
generator](https://github.com/ams-hackers/gbforth/blob/master/src/compiler/codegen.fs)
is invoked on the special word `main`. This, in turn, will invoke
recursively the code generator for any word that is required by main,
and so on. This means that only words that you actually use are
included in the ROM, saving considerable space. There are other ways
of forcing a word code to be generated, for example, trying to get its
address (`' dup constant dup-addr`).

As a consequence, we are free to define as many words in libraries as
we like without affecting users that are not using them.

### Playing with the compiler

You can see the intermediate representation of a word with the word
`xsee`. There is a file `src/compiler/debug.fs` which you can change
easily and run it with `gforth src/compiler/debug.fs` while you make
changes to the compiler.
