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

  const memory = new Uint8Array(65536);
  for (let offset = 0; offset <= 0xffff; offset++) {
    memory[offset] = gameboy._mmu.readByte(offset);
  }

  cb(gameboy, memory);
}

runTest(
  path.resolve(__dirname, "./test-dup.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x22);
    assert(gameboy._cpu.c === 0xe9);
    assert(memory[0xffe9] === 0x22)
    assert(memory[0xffeb] === 0x11)
  }
);

runTest(
  path.resolve(__dirname, "./test-swap.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x22);
    assert(gameboy._cpu.c === 0xe9);
    assert(memory[0xffe9] === 0x33)
    assert(memory[0xffeb] === 0x11)
  }
);

runTest(
  path.resolve(__dirname, "./test-drop.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x11);
    assert(gameboy._cpu.c === 0xed);
  }
);

runTest(
  path.resolve(__dirname, "./test-memget.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x66);
    assert(gameboy._cpu.c === 0xe9);
    assert(memory[0xffe9] === 0xed)
    assert(memory[0xffeb] === 0xce)
  }
);

runTest(
  path.resolve(__dirname, "./test-quadruple.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(gameboy._cpu.hl === 0x44);
    assert(gameboy._cpu.c === 0xed);
  }
);
