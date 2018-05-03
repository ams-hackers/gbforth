# dmg-forth

A Forth-based Game Boy development kit

## Install

### Dependencies

```
brew install watch colordiff gforth rgbds
```

### Build

To build the example rom and the Forth build rom run
```
make buildall
```

To just build the Forth rom run

```
make build
```

### Test

To run the test against the 2 rom's checksum
```
make test
```

This will calculate the checksum of the Forth rom and compares it  
with the example one (which is working on the emulator).  
In case they don't match a diff between the 2 `hexdump` is shown in the  
terminal (`colordiff` better highlights the diff).

It is possible to run a `watch` command which executes the `test` command  
every 2 seconds in order to have a report while refactoring the rom.

```
make watch
```
