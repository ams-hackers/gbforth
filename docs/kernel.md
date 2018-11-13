# Kernel

## Register allocation

**TOS:** Register `HL` contains the top of the parameter stack.

**PSP:** Register `C` contains the Parameter Stack Pointer.

**RSP:** Register `SP` contains the Return Stack Pointer.

## Memory allocation

**SP0:** The parameter stack pointer is initialised to address `$FFFE`, located
at the very end of the HRAM. The PSP value is *decreased* when a new item is
added to the stack.

**RS0:** The return stack pointer (RSP) is initialised to address `$CFFF`,
located at the very end of the RAM (or the end of RAM bank 0). The RSP value is
*decreased* when a new address is added to the return stack.

**DP:** The dictionary pointer is stored at addresses `$C000` and `$C001`,
located at the start of the RAM (or RAM bank 0). The pointer value is
initialised to `$C002`, and is *increased* when new memory is allocated.

## Pushing a value to the PSP

```forth
\ Move TOS "down" the stack
C dec,
H A ld, A [C] ld,
C dec,
L A ld, A [C] ld,

\ Load a value to the TOS
$1234 # HL ld,
```

## Popping a value from the PSP

```forth
\ Load TOS to register DE
H D ld,
L E ld,

\ Move second item to TOS
[C] A ld, A L ld,
C inc,
[C] A ld, A H ld,
C inc,
```
