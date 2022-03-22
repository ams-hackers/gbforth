SHELL := /bin/bash

ifneq (, $(shell which sha1sum))
SHA1 ?= sha1sum
else ifneq (, $(shell which shasum))
SHA1 ?= shasum
endif

GBFORTH = ./gbforth
export GBFORTH_PATH := $(shell pwd)/lib

LIB_FILES=lib/*.fs lib/core/*.fs
SOURCE_FILES=gbforth gbforth.fs src/*.fs src/utils/*.fs src/compiler/*.fs shared/*.fs

TEST_FILES = $(wildcard test/*.fs) $(wildcard test/*/*.fs)
TEST_OBJS = $(subst .fs,.gb,$(TEST_FILES))

EXAMPLE_OBJS = \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb \
	examples/sokoban/sokoban.gb \
	examples/simon/simon.gb \
	examples/aces-up/aces-up.gb \
	examples/happy-birthday/happy-birthday.gb \
	examples/10-print/10-print.gb \
	examples/brainfuck/brainfuck.gb \
	examples/synth/synth.gb

.PHONY: all examples tests

# Pattern rule to build gbforth roms
%.gb: %.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --pad-ff $< $@

#
# Examples
#
examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs examples/hello-world-asm/*.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --no-kernel $< $@

examples: $(EXAMPLE_OBJS)

#
# Tests
#
tests: $(TEST_OBJS)

check: tests examples
	@cd examples/hello-world-asm/ && $(SHA1) -c hello.gb.sha
	gforth src/asm.spec.fs -e bye
	( cd test/; yarn test )

#
# Utils
#
clean:
	-rm -f $(EXAMPLE_OBJS)
	-rm -f $(TEST_OBJS)

all: examples

#
# Docker commands
#
docker-build:
	docker build -t amshackers/gbforth .
