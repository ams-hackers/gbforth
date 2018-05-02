#!/bin/bash
set -e
./dmgforth.fs
# hexdump output.gb
diff -U 10 <(hexdump rgbds-hello-world/hello-world.gb) <(hexdump output.gb)
shasum -c output.sha
echo "YAY ✨"
