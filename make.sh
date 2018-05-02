set -e
./dmgforth.fs
hexdump output.gb
shasum -c output.sha
echo "YAY ✨"
