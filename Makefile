SHELL := /bin/bash

export DMGFORTH_PATH := $(shell pwd)/lib

.PHONY: all examples

all: examples

LIB_FILES=lib/*.fs
SOURCE_FILES=dmgforth src/*.fs

examples: \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb 

# Examples
examples/hello-world/hello.gb: examples/hello-world/hello.fs $(SOURCE_FILES) $(LIB_FILES)
	./dmgforth $< $@

examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs $(SOURCE_FILES) $(LIB_FILES)
	./dmgforth --no-kernel $< $@
	@cd examples/hello-world-asm/ && shasum -c hello.gb.sha

check:
	gforth src/asm.spec.fs -e bye

clean:
	-rm examples/hello-world-asm/hello.gb
	-rm examples/hello-world/hello.gb
