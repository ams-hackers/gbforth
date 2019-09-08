# Hello World!

Once you have gbforth [setup properly](./setup.md), and you have at least a
basic [understanding of Forth](./forth-crash-course.md), it's time to make your
first ROM! This guide will show you step-by-step how to get something simple
like _"Hello World"_ on the Game Boy LCD screen.

## 1. Entry point

Create a new file to start coding in: `hello.fs`. To compile a ROM, we need to
at least define the `main` word. This is the entry point that gbforth will
execute when loading the ROM.

To start simple, you can define an empty `main` word:

```fs
: main ;
```

If you are unfamiliar with Forth, here's a break-down of this code:

- `:` indicates that we want to define a new word (a _colon definition_)
- `main` is the name of this new word
- Following words make up the body of the definition (in our case it's empty)
- `;` indicates the end of the definition

This is the most minimal program we can write. To compile it from your terminal:

```sh
gbforth hello.fs
```

This will generate `hello.gb`. Of course this ROM will not do anything yet, but
gbforth is already taking care of setting up the headers and checksums that are
required to make the ROM valid and run on a real device.

## 2. Adding font tiles

The first thing we need to add are some graphics. The Game Boy supports drawing
tiles as sprites or on a background: Sprites are displayed on top of the
background, have transparency, and can be rendered at arbitrary positions.
Backgrounds are a bit more simple, and are always aligned to a 20x18 grid.

For the purpose of displaying some text, background tiles are a good choice, and
are easier to set up.

To make development a bit simpler, gbforth comes with a set of useful libs. One
of those libs is the [ibm-font.fs lib](./libs/ibm-font.fs). Let's include it by
adding the following line to your `hello.fs` file:

```fs
require ibm-font.fs
```

Including this lib will add 256 monochrome (2 colours) tiles to your ROM that
contain basic ASCII characters.

Next, we need to add some initialisation code to our `main` word:

```fs
: main install-font ;
```

The `install-font` word will copy the tile set to the video RAM (VRAM) at
run-time, which is required before we can draw any of the tiles to the screen.

## 3. Displaying text

If you are familiar with Forth, you might be aware that there is a word
available that lets you output strings to the terminal: `."`

On the Game Boy however, there is no terminal available. Luckily we can emulate
it! Another useful lib that comes with gbforth is the
[term.fs lib](./libs/term.fs), which allows you to use a virtual terminal.
Again, we include the lib at the top of our file:

```fs
require term.fs
```

Additionally, we need to initialise this lib by calling `init-term`:

```fs
: main
  install-font
  init-term ;
```

This makes sure the screen position is reset properly and the background is
cleared (otherwise the boot logo would still be visible).

Now that the term lib is set up, we can use `."` to output a string:

```fs
: main
  install-font
  init-term
  ." Hello World" ;
```

If you try to compile your `hello.fs` file now and run it, you should be greeted
with _"Hello World"_ in the top left corner of the LCD screen.

## 4. Positioning text

We can do a little bit better than that! The `term.fs` lib also defines the word
`at-xy`, which lets you adjust the cursor position before outputting text.

After some fiddling, you will find that positioning the cursor at `(3,7)` will
more or less center your greeting. Your final program should now look something
like this:

```fs
require ibm-font.fs
require term.fs

: main
  install-font
  init-term
  3 7 at-xy
  ." Hello World" ;
```

## 5. Running your ROM

If you want to test your ROM, you can either load it on an emulator, or use a
flashcart (or separate cartridge/flasher) to run it on a real device.
