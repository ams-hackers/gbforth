# Word Index

## Toplevel (compile-time)

A number of common Forth words are available from the *toplevel* context at
compile-time. Generally speaking you should be able to use all stack
(`dup`, `swap`, ...) and arithmetic/logic (`+`, `<>`, ...) operators as you
would expect them; they are simply exposed from the *HOST* vocabulary. If you
think a word is missing, consider
[opening an issue](https://github.com/ams-hackers/gbforth/issues/new) or submit
a PR to add it.

Words that operate on the memory are implemented in a way that they reference
the target RAM/ROM instead of your host memory. Other than that they should
behave similar to the
[ANS Forth specification](https://www.taygeta.com/forth/dpansf.html).

Additionally, the are a few gbforth specific words available to you at
compile-time:

##### `:` *( -- )*
Parses the next word and starts compiling a new *target definition*. Words
defined with `:` can not be executed at compile-time and are only available
from within other target definitions.

##### `:m` *( -- )*
Parses the next word and starts compiling a new *meta definition*. Words
defined with `:m` can not be executed at run-time and serve only as an
abstraction level for simplifying your toplevel code.

##### `;` *( -- )*
Ends the target or meta definition and switches back to interpreter mode.

##### `==>` *( n -- )*
Sets the ROM offset to a new position. Usually only needed when writing ASM.

##### `[asm]` *( -- )*
Adds the *ASSEMBLER* vocabulary to the context, allowing you to write ASM
instructions.

##### `[endasm]` *( -- )*
Removes the *ASSEMBLER* vocabulary from the context.

##### `[host]` *( -- )*
Switches to the *HOST* vocabulary, allowing you to use Forth words that operate
on your host machine and memory.

##### `[target]` *( -- )*
Switches to the *TARGET* vocabulary (this list). This is the default context.

##### `code` *( -- )*
Parses the next word and creates a new ASM primitive. The ASSEMBLER vocabulary
is made available automatically.

##### `endcode` *( -- )*
Ends the ASM primitive definition.

##### `-endcode` *( -- )*
Ends the ASM primitive definition without compiling a RETURN at the end. This
assumes that you handle exiting the definition in ASM yourself.

##### `gamecode:` *( -- )*
Parses the rest of the line and sets the cartridge Game Code. Maximum 4
characters (overwrites the last characters from the title if this exceeds 11
  characters).

##### `main:` *( -- )*
Patches the entry point vector (in the header of the cartridge) with a jump to
the current ROM offset. Usually only needed when writing an ASM-only game, as
the kernel overwrites this to point to the gbforth prelude by default.

##### `makercode:` *( -- )*
Parses the rest of the line and sets the cartridge Maker Code. Maximum 4
characters (overwrites the last characters from the title).

##### `mem,` *( addr u -- )*
Compiles the *host* memory starting at `addr` with length `u` to the target ROM.

##### `ram` *( -- )*
Switch to the RAM memory space (affects the words `here`, `unused`, `allocate`).
In this mode you are unable to access the memory (e.g. words like `@` and `!`
are unavailable), you can only allocate space (e.g. using `variable`).

##### `rom` *( -- )*
Switch to the ROM memory space (affects the words `here`, `unused`, `allocate`).
In this mode you are able use words like `!` and `c,`, but allocated space can
not be written to at run-time.

##### `title:` *( -- )*
Parses the rest of the line and sets the cartridge Title. Maximum 15 characters
(or 11 if you want to specify a Game Code as well).

> Todo:
>
> Words defined in standard but with slightly different behaviour?
>
> here / unused / allot / create /
> fill / erase / @ / ! /
> ' ( tick ) /
> 'body ( tick >body) /

## Target definitions (run-time)

For target definitions (defined with `:`) there is a large number of common
Forth words available from the core library (automatically included). These
words behave (as close as possible) according to the
[ANS Forth standard](https://www.taygeta.com/forth/dpansf.html). A few
differences exists that are related to [run-time limitations](./limitations.md)
or the [memory model](./memory.md) for the Game Boy.

Apart from the core words, a lot of additional useful words are available from
[libraries](./libs.md) that you can include in your project.
