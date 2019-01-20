# sfx.fs (lib)

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

##### `beep` *( -- )*
##### `boop` *( -- )*
##### `blip` *( -- )*
##### `pow` *( -- )*
##### `thud` *( -- )*
##### `buzz` *( -- )*
##### `whoosh` *( -- )*
