# Limitations

Compiling to and running on the Game Boy comes with a few limitations
(or alternative -- and maybe unexpected -- behaviour) compared to other Forth
systems.

## Run-time limitations

It is important to note that gbforth does not provide a run-time Forth system on
the target. While most words will be compiled and work on the target as you
would expect, this imposes some limitations compared to other Forth systems.

This section lists all words that are either unsupported completely, or behave
differently in order to emulate the expected behaviour as close as possible.

### Unsupported

| Word | Reason |
| ---- | ------ |
| `create` | No input stream available |
| `constant` | No input stream available |
| `variable` | No input stream available |
| `parse` | No input stream available |
| `postpone` | No input stream available |

### Partial support

| Word     | Alternative behaviour | Reason |
| -------- | --------------------- | ------ |
| `quit`   | Terminate execution of the program and _halt_ the CPU | No run-time available |
| `bye`    | Terminate execution of the system and _stop_ the CPU (this also disables the LCD) | No OS available |

## Compile-time limitations

Most available words will work according to the Forth standard during
compile-time. Most differences come from the fact that the target uses separate
[ROM and RAM memory spaces](./memory.md), which requires you to specify whether
to address the ROM or RAM for words operating on the memory. Furthermore, it is
not possible to access the RAM memory space during compile-time, so you are
restricted to only allocating memory there.

### Compiling strings (`s"` / `."`)
In order to save space, compiled strings are stored at the dictionary pointer.
This is usually not a problem, unless you are mixing the words `s"` or `."` with
other words that compile data to the **DP**. For example, in standard Forth you
might do this to store a string:

```forth
\ mem, copies the string to the correct location
CREATE message s" Hello World" mem,
```

But in gbforth you'll need to do:

```forth
\ already correctly placed, simply drop ( addr u )
CREATE message s" Hello World" 2drop
```

### Copying memory (`mem,`)
When writing to memory at compile-time, you are usually dealing with the ROM.
If you need to access the host memory instead, you need to explicitly specify
this using the word `[host]` (and `[target]` to return).

One exception is the (non-standard) word `mem,` ( addr u -- ), which takes data
from the host memory and compiles it to the dictionary pointer. The reasoning
behind this is that this is convenient (and something you'll often need), and
it's unlikely that you'd want to duplicate data in the ROM (with limited space).

Note: The words `move`, `cmove` and `cmove>` do not support this
cross-referencing of memory (yet?) and are
only available at run-time or in the host context.

### The stack
During compilation a lot of stack modifying words are available to you, including
most arithmetic operators. Although math is universal, the cell size of the data
stack is not. The most apparent difference is that the target system only supports
_integers_, not floating point numbers. Another -- more easily overlooked --
difference is the range of those integers. You can only use **16 bit integers**
at run-time. This might cause unexpected results due to overflowing values:

```forth
\ overflows to -23536
: foo ( -- true )
  #42000 0< ;

\ no overflow on host
: bar ( -- false )
  [ #42000 0< ]L ;

\ however...
: baz ( -- true )
  #42000 [ #42000 ]L = ;
```

Note: Leaving values on the stack does not result in the target system being
initialised with these values, and will result in an error during compilation.
