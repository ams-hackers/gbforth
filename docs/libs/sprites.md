# sprites.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/sprites.fs)

The `sprites.fs` library contains words that help work with sprites.

## Usage

Initialise the lib with `init-sprites` to clear the OAM and enable sprite drawing:

```forth
require sprites.fs

: main
  init-sprites ;
```

## Word Index

##### `SPRITE` _( u -- )_

Creates a new constant for a sprite at index _u_ (0-39).

##### `spr-x` _( c-addr -- c-addr )_

Returns the address of the X-coordinate of the given sprite.

##### `spr-y` _( c-addr -- c-addr )_

Returns the address of the Y-coordinate of the given sprite.

##### `spr-tile` _( c-addr -- c-addr )_

Returns the address of the tile index of the given sprite.

##### `spr-opts` _( c-addr -- c-addr )_

Returns the address of the attributes of the given sprite.

## Example

```forth
require sprites.fs

0 SPRITE player
1 SPRITE wall
2 SPRITE coin

: main
  init-sprites

  4 player spr-tile c!
  0 player spr-x    c!
  0 player spr-y    c!

  2 wall   spr-tile c!
  7 coin   spr-tile c! ;
```
