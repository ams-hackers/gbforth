SHELL := /bin/bash

export DMGFORTH_PATH := $(shell pwd)/lib

LIB_FILES=lib/*.fs
SOURCE_FILES=dmgforth src/*.fs
TESTS= \
	test/test-asm-add.gb \
	test/test-asm-sub.gb \
	test/test-colon-shadow.gb \
	test/test-double.gb \
	test/test-drop.gb \
	test/test-dup.gb \
	test/test-execute.gb \
	test/test-memget.gb \
	test/test-memset.gb \
	test/test-fetch.gb \
	test/test-store.gb \
	test/test-plus.gb \
	test/test-swap.gb

.PHONY: all examples tests

# Pattern rule to build dmg-forth roms
%.gb: %.fs $(SOURCE_FILES) $(LIB_FILES)
	./dmgforth $< $@

all: examples

#
# Examples
#
examples: \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb

examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs $(SOURCE_FILES) $(LIB_FILES)
	./dmgforth --no-kernel $< $@
	@cd examples/hello-world-asm/ && shasum -c hello.gb.sha

#
# Tests
#
check: tests examples
	gforth src/asm.spec.fs -e bye
	node test/test.js

tests: $(TESTS)

clean:
	-rm -f examples/hello-world-asm/hello.gb
	-rm -f examples/hello-world/hello.gb
	-rm -f test/test-asm-add.gb
	-rm -f test/test-asm-sub.gb
	-rm -f test/test-dup.gb
	-rm -f test/test-swap.gb
	-rm -f test/test-drop.gb
	-rm -f test/test-memget.gb
	-rm -f test/test-memset.gb
	-rm -f test/test-fetch.gb
	-rm -f test/test-store.gb
	-rm -f test/test-plus.gb
	-rm -f test/test-quadruple.gb
