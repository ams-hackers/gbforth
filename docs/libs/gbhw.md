# gbhw.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/gbhw.fs)

The `gbhw.fs` library contains data to help using the Game Boy hardware.

## Register addresses

Registers are simple constants that are prefixed with `r`. For example, `rDIV`
is the divider register, and `rSCY` the window scroll-y register.

Using these constants from any context simply returns the numeric value of
the address:

```
: reset-scroll
  0 rSCX c!
  0 rSCY c! ;
```

## Register references (ASM)

All the register addresses are also available as references for assembly code.
They have the same name, but are wrapped in square brackets (`[r...]`):

```
code reset-scroll
  0 # A ld,
  A [rSCX] ld,
  A [rSCY] ld,
endcode
```

## Memory addresses

Special memory addresses are listed as constants prefixed with `_`. For example,
`_CARTRAM` is the start of the cartridge RAM area, and `_AUD3WAVERAM` the start
of the memory containing the wave samples for audio channel 3.

## Flag bitmasks

Flags are simple constants that can be useful when working with register values.
They are prefixed with the register name (without the `r`), followed by `F_`.
For example, `STATF_BUSY` and `STATF_LYCF` are flags used with the `rSTAT`
register.

```
begin,
  [rSTAT] a ld,
  STATF_BUSY # A and,
#z until,
```

## Other values

There are a few other useful values available, such as `SCRN_X` (the width of
the screen in pixels). Refer to the [lib/gbhw.fs](../../lib/gbhw.fs) source to see the list of all
available constants.
