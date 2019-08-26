# Command Line Interface

CLI usage:

```sh
gbforth [<options>] input.fs [output.gb]
```

This takes `input.fs` as source code and creates the ROM at `output.gb`.

Additionally, `output.sym` is generated that lists all symbol names and
addresses. This file can be used by emulators to help with debugging.

## Options

#### --no-kernel

This excludes the kernel, only allowing you to use assembler.

#### -d, --debug

Enables debugging mode. This returns control to the REPL after compilation has
finished.

#### -v, --verbose

Enables verbose output while compiling.

#### -p, --pad-ff

Pad the ROM with `$FF` instead of `$00`. Depending on the hardware you are
using, this may improve the speed of flashing the cartridge.
