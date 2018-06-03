## dmg-forth assembler

dmg-forth includes an assembler. This is useful when you want more
control about how the hardware is used or you need to optimize some
piece of code.

You can combine Forth and Assembler as you please. You can even write
all your game in assembler, yet having the power of all the Forth at
compile-time.

### Introduction

dmg-forth's assembler lives in a separate *vocabulary*, which can be accessed using the words `[asm]` and `[endasm]`.

It provides a traditional postfix notation. Operands are specified
first, and then the instruction. For example, the code below will move
the number 42 to the register `HL`.

```forth
[asm]

42 # HL ld,

[endasm]
```

To learn more, you can have a look to the hello world example
[written in assembler](https://github.com/ams-hackers/dmg-forth/blob/master/examples/hello-world-asm/hello.fs),
or the [the lib/ directory](https://github.com/ams-hackers/dmg-forth/tree/master/lib).


### Operands

Operands are kept in an auxiliary stack.

#### Immediate

You can push immediate values by using the word `#`. You can use
prefixes to specify the base of your immediate values.

```forth
[asm]

42 # HL ld,     \ decimal
$ff # HL ld,    \ hex
%1011 # HL ld,  \ binary

[endasm]
```

#### Registers

Registers like `A`, `B`, `SP`, or `HL` push themselves to the operand
stack.

Some registers also support memory indirection. In that case, the
register is usually wrapped around brackets: `[HL]`, `[C]`, `[DE]`.

Finally, there are the operands `[HI+]` and `[HI-]`. This means that
the register `HI` will be used to reference the memory, and then the
register will be incremented or decremented.

#### Memory references

Additionally, memory references can be specified like `$FF40 ]*`


### Labels

TBW.
