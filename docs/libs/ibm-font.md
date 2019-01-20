# ibm-font.fs (lib)

The `ibm-font.fs` library contains a basic tilemap for displaying text on the
screen.

## Usage

Initialise the lib with `install-font`, this copies the tilemap to the VRAM:

```forth
require ibm-font.fs

: main
  install-font ;
```
