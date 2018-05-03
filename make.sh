#!/bin/bash
set -e
./dmgforth.fs
# hexdump output.gb
colordiff -U 2 <(hexdump -v rgbds-hello-world/hello-world.gb) <(hexdump -v output.gb)
shasum -c output.sha
echo "YAY ✨"
