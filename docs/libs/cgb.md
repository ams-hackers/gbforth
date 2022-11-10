# cgb.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/cgb.fs)

The `cgb.fs` library contains words that help implement color support.

## Usage

## Word Index

##### `enable-cgb` _( -- )_

Use at toplevel to mark the cartridge as CGB compatible. Note that the default
header marks the cartridge as a regular DMG game, which disables color related
features when run on a CGB device.

##### `detect-cgb` _( -- f )_

Returns `true` if the game is running on a CGB device. Note that this word only
works correctly before you write anything to the RAM, so it is recommended to
check this flag immediately in your `main` definition, and manually store the
value in a variable for later retrieval.
