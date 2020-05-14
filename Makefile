SHELL := /bin/bash

ifneq (, $(shell which sha1sum))
SHA1 ?= sha1sum
else ifneq (, $(shell which shasum))
SHA1 ?= shasum
endif

export GBFORTH_PATH := $(shell pwd)/lib

LIB_FILES=lib/*.fs lib/core/*.fs
SOURCE_FILES=gbforth src/*.fs src/utils/*.fs src/compiler/*.fs shared/*.fs

TEST_FILES = $(wildcard test/*.fs) $(wildcard test/*/*.fs)
TEST_OBJS = $(subst .fs,.gb,$(TEST_FILES))

GBFORTH = ./gbforth

.PHONY: all examples tests

# Pattern rule to build gbforth roms
%.gb: %.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --pad-ff $< $@

all: examples

#
# Examples
#
examples: \
	examples/hello-world-asm/hello.gb \
	examples/hello-world/hello.gb \
	examples/sokoban/sokoban.gb \
	examples/simon/simon.gb \
	examples/aces-up/aces-up.gb \
	examples/happy-birthday/happy-birthday.gb \
	examples/10-print/10-print.gb \
	examples/brainfuck/brainfuck.gb \
	examples/synth/synth.gb

examples/hello-world-asm/hello.gb: examples/hello-world-asm/hello.fs examples/hello-world-asm/*.fs $(SOURCE_FILES) $(LIB_FILES)
	$(GBFORTH) --no-kernel $< $@

#
# Tests
#
check: tests examples
	@cd examples/hello-world-asm/ && $(SHA1) -c hello.gb.sha
	gforth src/asm.spec.fs -e bye
	( cd test/; yarn test )

tests: $(TEST_OBJS)

clean:
	-rm -f examples/hello-world-asm/hello.gb
	-rm -f examples/hello-world/hello.gb
	-rm -f examples/sokoban/sokoban.gb
	-rm -f examples/simon/simon.gb
	-rm -f examples/aces-up/aces-up.gb
	-rm -f examples/happy-birthday/happy-birthday.gb
	-rm -f examples/10-print/10-print.gb
	-rm -f examples/brainfuck/brainfuck.gb
	-rm -f examples/synth/synth.gb
	-rm -f $(TEST_OBJS)

#
# Docker commands
#
docker-build:
	docker build -t amshackers/gbforth .
