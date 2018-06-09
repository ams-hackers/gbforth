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

Operands are kept in an auxiliary stack. Registers and flags push themselves
to this stack, immediate values have to be pushed manually.

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
register is wrapped in square brackets: `[HL]`, `[C]`, `[DE]`.

Finally, there are the operands `[HI+]` and `[HI-]`. This means that
the register `HI` will be used to reference the memory, and then the
register `HI` will be incremented or decremented.

#### Flags

Flags, like registers, push themselves to the operand stack.

There are 4 operands available to make an instruction conditional:
- `#Z` checks whether the zero flag is set
- `#NZ` checks whether the zero flag is **not** set
- `#C` checks whether the carry flag is set
- `#NZ` checks whether the carry flag is **not** set

These flags can be used in combination with the instructions `call,`, `jp,`, `jr,` and `ret,`.

```forth
[asm]

$1234 # #NZ jp,  \ jump if last result was non-zero
#C ret,          \ return if carry flag is set

[endasm]
```

#### Memory references

Memory references can be pushed to the operand stack using the word `]*`.

```forth
[asm]

$FF40 ]* A ld,  \ load value at address $FF40 into A

[endasm]
```

### Labels

Labels can be created using the word `label`. These labels are simple constants,
and can only be referenced *after* they are defined:

```
[asm]

label countToZero
A dec,
countToZero #NZ jp,

[endasm]
```

For local references however (like in the example above), you are encouraged to make use of two other concepts that the dmg-forth assembler provides: *stack-based references*, and *structured control flow*. These also support cases where you need to forward-reference an address (which is not possible with `label`).

#### Anonymous stack-based references

dmg-forth's assembler provides word pairs to create anonymous stack-based references:
- `here<` and `<there` for backward references
- `there>` and `>here` for forward references

They can be used to implement simple loops or jumps,
when there is no need to give a name to the reference:

```forth
[asm]

( backward reference )
here<
A dec,
<there #NZ jp,

( forward reference )
there> #C jp,
B inc,
>here

[endasm]
```

For cases where you need a long forward reference, you can use the word `named-ref>`
to assign a name to the reference:

```forth
[asm]

there> #Z jp,
named-ref> longFwdJump

( ... )

longFwdJump \ replaces >here

[endasm]
```

#### Structured control flow

The assembler exposes a few words similar to control flow words available in Forth:
- `begin,` ... `repeat,`
- `begin,` ... `until,`
- `begin,` ... `while,` ... `repeat,`
- `if,` ... `then,`
- `if,` ... `else,` ... `then,`

The words `if,`, `until,` and `while,` have to be combined with one of the flag operands.

These can be used to structure the control flow without using the underlying references directly:

```forth
[asm]

begin,
  A dec,
#Z until,

#NC if,
  B inc,
else,
  B dec,
then,

[endasm]
```
