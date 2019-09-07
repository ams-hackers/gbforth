# Stack effects

While forth has no type system, it is convention to describe the stack effect
of words in comments. You will find these stack effects throughout the code base
and in the documentation.

The comments follow the `( in -- out )` format, with the rightmost value being
the top of the parameter stack. To make it more clear what values you can expect,
the following symbols are used:

- `c` - Character (byte)
- `x` - Cell (2 bytes)
- `n` - Signed integer
- `u` - Unsigned integer
- `f` - Boolean flag (`true` or `false`)
- `c-addr` Char-aligned address
- `a-addr` Cell-aligned address
- `xt` - Execution token (address of a word)

In some cases, symbols are followed by a number, for example `nip` has the stack
effect `( x1 x2 -- x2 )` to make it clear that not the top of the stack is dropped,
but the second value on the stack. Without this addition, the effect would be
indistinguishable from `drop`, which has the effect `( x -- )`.
