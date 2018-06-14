# Code Words

Code words are the most primitives words in gbforth, and are defined in [assembler](./assembler.md).
The *core library* contains a lot of these primitives already, but sometimes you
may need to write new code words yourself.

You can define new code words using the `code` and `end-code` word. For example,
`c@` can be defined as follows:

```forth
( c-addr -- x )
code c@
  [HL] L ld,  \ HL register contains TOS
  $0 # H ld,  \ clear the higher byte
  ret,
end-code
```

When defining new code words, keep the memory and register allocations for the
[kernel](./kernel.md) in mind. Overwriting values in reserved registers (without
restoring them properly) might lead to unexpected behaviour.
