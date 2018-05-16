SHELL := /bin/bash

.PHONY: all examples

all: examples

LIB_FILES=lib/*.fs
SOURCE_FILES=dmgforth src/*.fs

examples: examples/hello-world/hello.gb

# Examples
examples/hello-world/hello.gb: examples/hello-world/hello.fs $(SOURCE_FILES) $(LIB_FILES)
	./dmgforth $< $@
	@colordiff -U 2 <(hexdump -v rgbds-hello-world/hello-world.gb) <(hexdump -v examples/hello-world/hello.gb)
	@cd examples/hello-world/ && shasum -c hello.gb.sha

check:
	gforth src/asm.spec.fs -e bye

clean:
	-rm examples/hello-world/hello.gb
