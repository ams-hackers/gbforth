#!/bin/bash
set -e
./dmgforth game.fs

# hexdump output.gb
colordiff -U 2 <(hexdump -v rgbds-hello-world/hello-world.gb) <(hexdump -v output.gb)
shasum -c output.sha
echo "YAY ✨"
