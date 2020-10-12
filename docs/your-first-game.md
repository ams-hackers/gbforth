# Your first game

Once you are comfortable writing [your first rom](./hello-world.md), it's time
to write a proper game!

In this guide, we'll write a **Simon**-like game: The player is presented with a
pattern of notes that they should memorise and play back after each round. To
keep things simple, I'll cover the essential parts only:

1. [Playing music notes](#1-playing-music-notes)
2. [Generating a pattern](#2-generating-a-pattern)
3. [Showing the pattern](#3-showing-the-pattern)
4. [Verifying player input](#4-verifying-player-input)
5. [Keeping track of score](#5-keeping-track-of-score)
6. [Feature suggestions](#6-feature-suggestions)

If you want to skip ahead and see what we'll be making, check out the
[simon.fs](https://github.com/ams-hackers/gbforth/blob/master/examples/simon/simon.fs)
game in the `examples/` folder.

## 1. Playing music notes

Start by creating a new file `simon.fs` to write our game in. We can start with
playing music notes first. Let's import the [music.fs lib](./libs/music.md) to
help with that:

```fs
require music.fs
```

This lib does not require any initialisation before using, and we directly have
access to a bunch of notes (from `C3` to `B8`) and the `note` word to play them.
You can try it out by playing a couple of notes in the `main` definition:

```fs
require music.fs

: main
  C#5 note
  E5  note
  A5  note
  E6  note ;
```

If you compile this ROM and run it, you'll notice that you can only hear a
single sound. This is because the 4 notes are played at almost the same time. To
make it sound properly, we need to delay some of the notes. For this we can use
the [time.fs lib](./libs/time.md):

```fs
require music.fs
require time.fs

: main
  C#5 note 500 ms
  E5  note 500 ms
  A5  note 500 ms
  E6  note ;
```

The `ms` word will wait a number of milliseconds before continuing. With this
addition you should be able to hear 4 distinguishable notes. Feel free to spend
some time playing with a couple of other notes or even trying to create a simple
melody!

## 2. Generating a pattern

The next step would be to generate a pattern of notes. To do this, we first need
to know how to generate a random note, so our game will not be too predictable.
After that, we need to save the pattern somewhere so we can verify that the
player remembers it correctly.

### Random notes

To be able to generate random values, we will include the
[random.fs lib](./libs/random.md), and set an initial seed. For now, you can
just store an arbitrary number (except for `0`) to the `seed` variable:

```fs
require random.fs

: main
  1234 seed ! ;
```

After this, you can use the word `random` to generate random numbers. Let's
define a new word `random-note` first to try this out:

```fs
: random-note ( -- n )
  4 random ;
```

Now we can add a `CASE` construction to map this number to a note:

```fs
: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;
```

Playing a single note isn't too exciting, so we can use `?DO`/`LOOP` to play a
random note a few times:

```fs
require random.fs
require music.fs
require time.fs

: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;

: main
  1234 seed !
  10 0 ?DO
    random-note note
    500 ms
  LOOP ;
```

Running this should play 10 random tones in succession. Give it a try!

### Storing the pattern

To remember the pattern, we need to reserve some space in the RAM to store it
in. First we use `CREATE` to define the memory space:

```fs
CREATE pattern
10 cells allot
```

The first line will define the word `pattern`, which pushes the memory address
of itself to the stack upon calling. The second line reserves 10 cells in the
memory for us to store our pattern in.

To save the first note, we can simply store it at the address returned by
`pattern`. Let's define a new word `generate-pattern` to do this:

```fs
: generate-pattern ( -- )
  random-note pattern ! ;
```

The second note needs to be written to the following cell, and the third note to
the cell following that one:

```fs
: generate-pattern ( -- )
  random-note pattern           !
  random-note pattern 1 cells + !
  random-note pattern 2 cells + ! ;
```

You can probably spot the pattern here already. To make things a bit nicer, we
will use the loop from before to fill the pattern with notes completely:

```fs
: generate-pattern ( -- )
  10 0 ?DO
    random-note pattern I cells + !
  LOOP ;
```

The word `I` pushes the index of the loop on top of the stack (`0`...`9`), so
this code will fill our `pattern` with 10 random notes.

We can now write a very similar word `play-pattern` as well:

```fs
: play-pattern ( -- )
  10 0 ?DO
    pattern I cells + @
    note
    500 ms
  LOOP ;
```

Rather than storing notes, this will fetch a note from the pattern, play it, and
wait 500 ms before playing the next one.

Our complete code will now look something like this:

```fs
require random.fs
require music.fs
require time.fs

CREATE pattern
10 cells allot

: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;

: generate-pattern ( -- )
  10 0 ?DO
    random-note pattern I cells + !
  LOOP ;

: play-pattern ( -- )
  10 0 ?DO
    pattern I cells + @
    note
    500 ms
  LOOP ;

: main
  1234 seed !
  generate-pattern
  play-pattern ;
```

## 3. Showing the pattern

Now we have generated a random pattern and are able to play it, we should also
add some visual feedback. Without this, the player would not know which buttons
to press. We can use the [ibm-font.fs](./libs/ibm-font.md) and the
[term.fs lib](./libs/term.md) to show a minimalistic UI. Include the libs and
initialise them first:

```fs
require ibm-font.fs
require term.fs

: main
  install-font
  init-term ;
```

Then we can define a new word that shows which key to press for a given note:

```fs
: .note ( n -- )
  CASE
    C#5 OF 24 ENDOF
    E5  OF 25 ENDOF
    A5  OF 26 ENDOF
    E6  OF 27 ENDOF
  ENDCASE
  emit ;
```

Depending on the note, this word will emit a character to the screen. The
numbers `24` to `27` are are character codes that correspond to the arrow
symbols in the ibm-font.

Now we can add this word to our `play-pattern` definition:

```fs
: play-pattern ( -- )
  10 0 ?DO
    pattern I cells + @
    dup
    9 8 at-xy .note
    note
    500 ms
  LOOP ;
```

We introduce `dup` as well to duplicate the note value, so we can pass it to
both the `.note` and `note` word. The `9 8 at-xy` part ensures we display the
arrow symbol at roughly the centre of the screen, and that the previous gets
overwritten (by always emitting to the same coordinates).

The full code so far:

```fs
require ibm-font.fs
require term.fs
require random.fs
require music.fs
require time.fs

CREATE pattern
10 cells allot

: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;

: generate-pattern ( -- )
  10 0 ?DO
    random-note pattern I cells + !
  LOOP ;

: .note ( n -- )
  CASE
    C#5 OF 24 ENDOF
    E5  OF 25 ENDOF
    A5  OF 26 ENDOF
    E6  OF 27 ENDOF
  ENDCASE
  emit ;

: play-pattern ( -- )
  10 0 ?DO
    pattern I cells + @
    dup
    9 8 at-xy .note
    note
    500 ms
  LOOP ;

: main
  install-font
  init-term
  1234 seed !
  generate-pattern
  play-pattern ;
```

## 4. Verifying player input

Now we're ready to ask the player for input! For this we'll need the help of the
[input.fs lib](./libs/input.md):

```fs
require input.fs

: main
  init-input ;
```

The `init-input` word makes sure the system is ready to accept key presses,
which we can get with the word `key`. We need to add a new word to convert a key
code to a note. It's a bit similar to the `.note` word we wrote earlier:

```fs
: key>note ( c -- n )
  CASE
    k-up    OF C#5 ENDOF
    k-down  OF E5  ENDOF
    k-right OF A5  ENDOF
    k-left  OF E6  ENDOF
               G3
  ENDCASE ;
```

In this word we are mapping from key code (`k-up`...`k-left`) to a note, making
sure that the keys are corresponding to the correct character codes we used in
`.note`.

We are also adding an extra note at the end of our 4 `OF`...`ENDOF` cases. We do
this because we want to return a different note if the player presses a key that
is not one of the joy pad keys. In this case `G3`, which is not a note that can
occur in the generated pattern (so this is always considered incorrect).

Let's define a new word that repeatedly checks for player input, and plays the
corresponding note:

```fs
: prompt-pattern ( -- )
  10 0 ?DO
    key key>note note
  LOOP ;
```

This will play 10 notes depending on what buttons you press. We also need to
verify that the entered notes match the pattern:

```fs
: prompt-pattern ( -- )
  10 0 ?DO
    key key>note
    dup note
    pattern I cells + @
    <> IF ." Game Over" bye THEN
  LOOP ;
```

We introduce `dup` again so we have a copy of the entered note. Then `note` will
play the note and consume the copied value.

After playing the entered note, we fetch the I-th note from the pattern (using
the same line as in `play-pattern`), and compare it to the entered note with the
word `<>`. This word compares 2 values, and returns `true` when they are not
equal to each other (in this case, if the player entered a wrong note).

In the `IF`/`THEN` block that follows the comparison, we display _"Game Over"_
to the screen. The word `bye` will stop the program entirely, so no more notes
can be entered once the player has lost.

Let's add the input handling to the rest of our code:

```fs
require ibm-font.fs
require term.fs
require input.fs
require random.fs
require music.fs
require time.fs

CREATE pattern
10 cells allot

: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;

: generate-pattern ( -- )
  10 0 ?DO
    random-note pattern I cells + !
  LOOP ;

: .note ( n -- )
  CASE
    C#5 OF 24 ENDOF
    E5  OF 25 ENDOF
    A5  OF 26 ENDOF
    E6  OF 27 ENDOF
  ENDCASE
  emit ;

: play-pattern ( -- )
  10 0 ?DO
    pattern I cells + @
    dup
    9 8 at-xy .note
    note
    500 ms
  LOOP ;

: key>note ( c -- n )
  CASE
    k-up    OF C#5 ENDOF
    k-down  OF E5  ENDOF
    k-right OF A5  ENDOF
    k-left  OF E6  ENDOF
               G3
  ENDCASE ;

: prompt-pattern ( -- )
  10 0 ?DO
    key key>note
    dup note
    pattern I cells + @
    <> IF ." Game Over" bye THEN
  LOOP ;

: main
  install-font
  init-term
  init-input
  1234 seed !
  generate-pattern
  play-pattern
  prompt-pattern ;
```

## 5. Keeping track of score

If you run the code that we wrote so far, you'll notice that we are pretty far
already: A melody is generated and presented to the player, and the player can
enter the pattern which is also verified.

To turn this into a proper game, we need to make the pattern increase in length
1 note at a time, rather than playing 10 notes from the beginning. A quick way
to do this, is by _parameterising_ the `play-pattern` and `prompt-pattern`:
Remove the `10` at the start of the definition of both words, and move them to
the `main` word:

```fs
: play-pattern ( u -- )
  0 ?DO
  ( ... ) ;

: prompt-pattern ( u -- )
  0 ?DO
  ( ... ) ;

: main
  ( ... )
  10 play-pattern
  10 prompt-pattern ;
```

Due to the concatenative nature of Forth, this code still does the same thing.
The big difference is that we can now tweak the amount of notes we want to play
and prompt. To start with a single note, and increase the length every round, we
can use a loop:

```fs
: main
  ( ... )
  11 1 ?DO
    I play-pattern
    I prompt-pattern
  LOOP ;
```

This way we call both words repeatedly, passing the value `1` to `10` to
indicate the current pattern length.

At the end of each round, we should probably show the current score:

```fs
: main
  ( ... )
  11 1 ?DO
    I play-pattern
    I prompt-pattern
    2 2 at-xy
    ." Score: "
    I .
  LOOP ;
```

First we move the cursor to coordinates `(2,2)`, and display a string to the
player. Then we use `I` to get the current loop index (which is the current
round), and display the number as text with the `.` word.

It would also be a good idea to add a message for when the player completed all
10 rounds:

```fs
: main
  ( ... )
  11 1 ?DO
    I play-pattern
    I prompt-pattern
    2 2 at-xy
    ." Score: "
    I .
  LOOP
  2 3 at-xy ." You win!" ;
```

And that's it! We have finished creating a very simple game for the Game Boy.
There is a lot of room for improvement still, but it has a basic game loop,
score system, and win/lose condition. Our final code looks like this:

```fs
require ibm-font.fs
require term.fs
require input.fs
require random.fs
require music.fs
require time.fs

CREATE pattern
10 cells allot

: random-note ( -- n )
  4 random CASE
    0 OF C#5 ENDOF
    1 OF E5  ENDOF
    2 OF A5  ENDOF
    3 OF E6  ENDOF
  ENDCASE ;

: generate-pattern ( -- )
  10 0 ?DO
    random-note pattern I cells + !
  LOOP ;

: .note ( n -- )
  CASE
    C#5 OF 24 ENDOF
    E5  OF 25 ENDOF
    A5  OF 26 ENDOF
    E6  OF 27 ENDOF
  ENDCASE
  emit ;

: play-pattern ( u -- )
  0 ?DO
    pattern I cells + @
    dup
    9 8 at-xy .note
    note
    500 ms
  LOOP ;

: key>note ( c -- n )
  CASE
    k-up    OF C#5 ENDOF
    k-down  OF E5  ENDOF
    k-right OF A5  ENDOF
    k-left  OF E6  ENDOF
               G3
  ENDCASE ;

: prompt-pattern ( u -- )
  0 ?DO
    key key>note
    dup note
    pattern I cells + @
    <> IF ." Game Over" bye THEN
  LOOP ;

: main
  install-font
  init-term
  init-input
  1234 seed !
  generate-pattern
  11 1 ?DO
    I play-pattern
    I prompt-pattern
    2 2 at-xy
    ." Score: "
    I .
  LOOP
  2 3 at-xy ." You win!" ;
```

## 6. Feature suggestions

If you followed along every step, you should now have a minimalistic but
functioning game. A good way to learn more is by trying to implement some extra
features or improvements yourself. Here are some suggestions (with increasing
difficulty) to work on next:

- The game is not aesthetically pleasing at all. You can use `at-xy` and `page`
  to improve the UI rendering, and `ms` to delay some state transitions a bit.
- For some players 10 rounds might be a bit too easy to beat. You could increase
  this to 20, or even 100.
- Still not challenging enough? You could add more variety to the pattern by
  introducing 2 extra notes and using the `k-a` and `k-b` keys.
- Because we use a constant number as the initial seed, the pattern is not
  really random. One approach to improve the randomness is to add a title
  screen, wait for a key press, and use `utime` as the initial seed.
- After you win or lose, there is no way to play the game again other than
  restarting the device completely. You could improve this by restarting the
  game loop after it has finished, or even adding a proper menu.
