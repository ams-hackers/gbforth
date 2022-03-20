# gbforth [![Build Status](https://github.com/ams-hackers/gbforth/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/ams-hackers/gbforth/actions/workflows/ci.yml?query=branch%3Amain)

A Forth-based Game Boy development kit

It features a Forth-based assembler, a cross-compiler with support
for lazy code generation and a library of useful words.

[Read the documentation](https://ams-hackers.github.io/gbforth/).

## Contributing

The best way to contribute would be try to write a game! Then we can,
at the same time, improve gbforth to make it more useful.

For more detailed guidelines **[read the contribution guide](https://ams-hackers.github.io/gbforth/contribute)**.

Also have a look to the issues in GitHub:

- [General issues](https://github.com/ams-hackers/gbforth/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+-label%3Aconformance+)
- [ANS Conformance issues](https://github.com/ams-hackers/gbforth/milestones)

## Install

### Dependencies

```
brew install gforth
```

### Build

To build the examples, run

```
make examples
```

### Tests

To run the tests, you need to have `nodejs` and `yarn` installed. Make
sure to install the dependencies in the `test/` directory first:

```
( cd test/; yarn )
```

After that you can run the tests:

```
make check
```

## Running with Docker

You can run gbforth in Docker using the [amshackers/gbforth](https://hub.docker.com/r/amshackers/gbforth) image:

```
docker run amshackers/gbforth
```

To compile your project, mount your project directory as a volume and
pass the input and output file as CLI arguments:

```
docker run --rm -v "$PWD":/data amshackers/gbforth game.fs game.gb
```
