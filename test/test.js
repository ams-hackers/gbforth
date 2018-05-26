"use strict";

const assert = require("assert");
const path = require("path");
const fs = require("fs");
const Gameboy = require("node-gameboy");

function runTest(rompath, { cycles }, cb) {
  const gameboy = new Gameboy();

  gameboy.loadCart(fs.readFileSync(rompath));
  gameboy._init();

  for (let i = 0; i < cycles; i++) {
    gameboy._cpu._runCycle();
  }

  const memory = new Int8Array(65536);
  for (let offset = 0; offset <= 0xffff; offset++) {
    memory[offset] = gameboy._mmu.readByte(offset);
  }

  cb(gameboy, memory);
}

runTest(
  path.resolve(__dirname, "./test-dup.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x44);
    assert(gameboy._cpu.c === 0xed);
  }
);