# ibm-font.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/ibm-font.fs)

The `ibm-font.fs` library contains a basic tilemap for displaying text on the
screen.

## Usage

Initialise the lib with `install-font`, this copies the tilemap to the VRAM:

```forth
require ibm-font.fs

: main
  install-font ;
```
