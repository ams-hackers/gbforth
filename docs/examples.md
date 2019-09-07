# Examples

We included a few examples in the `examples/` folder. To build the ROM files,
run the following from your terminal:

```
make examples
```

## Goto 10 [☞](https://github.com/ams-hackers/gbforth/blob/master/examples/goto10/goto10.fs)

The gbforth version of the classic BASIC one-liner `10 PRINT CHR$(205.5+RND(1)); : GOTO 10`. Produces a random maze-like pattern on the screen.

## Happy Birthday [☞](https://github.com/ams-hackers/gbforth/blob/master/examples/happy-birthday/happy-birthday.fs)

Written for the Game Boy's 30th anniversary. Uses the [music](./libs/music.md) lib to play _Happy Birthday_ and prints the lyrics to the screen.

## Hello World ASM [☞](https://github.com/ams-hackers/gbforth/blob/master/examples/hello-world-asm/hello.fs)

The result of decompiling an example ROM at the very start of this project. Uses plain [assembly](./assembler.md) to print _"Hello World !"_ to the screen.

## Hello World [☞](https://github.com/ams-hackers/gbforth/blob/master/examples/hello-world/hello.fs)

The most basic introduction to gbforth -- essentially a Forth rewrite from the ASM version. Uses the [term](./libs/term.md) lib to print _"Hello World !"_ to the screen.

## Sokoban [☞](https://github.com/ams-hackers/gbforth/blob/master/examples/sokoban/sokoban.fs)

Bernd Paysan wrote this code [in 1995 for gforth](https://git.net2o.de/bernd/gforth/blob/d22e8bc461061d539e13057d188462ce6b423683/sokoban.fs). We modified it slightly (but as little as possible) to make it run on the Game Boy. The changes are limited to adding `ROM` and `RAM` to control memory locations, moving some definitions into meta definitions (`:m ... ;`), and adding a little initialisation code to `main`. Most levels are commented out to make the game fit on a 32 KB cartridge, and the UI strings are shortened to fit the LCD display.
