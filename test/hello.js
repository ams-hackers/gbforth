"use strict";

const assert = require("assert");
const path = require("path");
const fs = require("fs");
const Gameboy = require("node-gameboy");

const gameboy = new Gameboy();

const rompath = path.resolve(__dirname, "../examples/hello-world/hello.gb");

function runCycles(count) {
  for (let i = 0; i < count; i++) {
    gameboy._cpu._runCycle();
  }
}

function getMemory() {
  const array = new Int8Array(65536);
  for (let offset = 0; offset <= 0xffff; offset++) {
    array[offset] = gameboy._mmu.readByte(offset);
  }
  return array;
}

gameboy.loadCart(fs.readFileSync(rompath));
gameboy._init();
runCycles(200);

assert(gameboy._cpu.hl === 0x44);
