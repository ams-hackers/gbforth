# gbforth [![Build Status](https://travis-ci.org/ams-hackers/gbforth.svg?branch=master)](https://travis-ci.org/ams-hackers/gbforth)
A Forth-based Game Boy development kit

It features a an Forth-based assembler, a cross-compiler with support
for lazy code generation and a library of useful words.

You can read the [documentation](https://ams-hackers.github.io/gbforth/).

The best way to contribute would be try to write a game! Then we can,
at the same time improve gbforth to make it more useful.

If you would like to contribute, please have a look to the issues in
github:
  - [General issues](https://github.com/ams-hackers/gbforth/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+-label%3Aconformance+)
  - [ANS Comformance issues](https://github.com/ams-hackers/gbforth/milestones)


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

To run the tests, run
```
make check
```
