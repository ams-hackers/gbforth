# sfx.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/sfx.fs)

The `sfx.fs` library contains a few basic sound effect presets.

## Usage

Initialise the lib with `init-sfx` before using any of the other words. This
will enable the sound channels and set the volume:

```forth
require sfx.fs

: main
  init-sfx ;
```

## Word Index

Run any of these words to play the corresponding sound effect immediately:

##### `beep` _( -- )_

##### `boop` _( -- )_

##### `blip` _( -- )_

##### `pow` _( -- )_

##### `thud` _( -- )_

##### `buzz` _( -- )_

##### `whoosh` _( -- )_
