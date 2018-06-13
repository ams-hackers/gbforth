SHELL := /bin/bash

export DMGFORTH_PATH := $(shell pwd)/lib

LIB_FILES=lib/*.fs
SOURCE_FILES=dmgforth src/*.fs

TEST_FILES = $(wildcard test/test-*.fs)
TEST_OBJS = $(subst .fs,.gb,$(TEST_FILES))

DMGFORTH = ./dmgforth $(DMGFORTH_FLAGS)

.PHONY: all examples tests

# Pattern rule to build dmg-forth roms
%.gb: %.fs $(SOURCE_FILES) $(LIB_FILES)
	$(DMGFORTH) $< $@

all: examples

#
# Examples
#
examples: \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb

examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs examples/hello-world-asm/*.fs $(SOURCE_FILES) $(LIB_FILES)
	$(DMGFORTH) --no-kernel $< $@
	@cd examples/hello-world-asm/ && shasum -c hello.gb.sha

#
# Tests
#
check: tests examples
	gforth src/asm.spec.fs -e bye
	( cd test/; yarn test )

tests: $(TEST_OBJS)

clean:
	-rm -f examples/hello-world-asm/hello.gb
	-rm -f examples/hello-world/hello.gb
	-rm -f test/test-*.gb
