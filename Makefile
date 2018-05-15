nSHELL := /bin/bash

.PHONY: build build-org buildall test watch

build-org:
	$(MAKE) -C rgbds-hello-world

output.gb:
	./dmgforth.fs

build: output.gb
buildall: build-org build

check:
	gforth src/asm.spec.fs -e bye

test:
	@$(MAKE) build
	@colordiff -U 2 <(hexdump -v rgbds-hello-world/hello-world.gb) <(hexdump -v output.gb)
	@shasum -c output.sha
	@echo "YAY ✨"

watch:
	watch --color $(MAKE) test

