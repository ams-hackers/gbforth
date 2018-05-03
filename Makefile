SHELL := /bin/bash

build-org:
	$(MAKE) -C rgbds-hello-world

build:
	./dmgforth.fs

buildall:
	$(MAKE) build-org
	$(MAKE) build

test:
	@$(MAKE) build
	@colordiff -U 2 <(hexdump -v rgbds-hello-world/hello-world.gb) <(hexdump -v output.gb)
	@shasum -c output.sha
	@echo "YAY ✨"

watch:
	watch --color $(MAKE) test
