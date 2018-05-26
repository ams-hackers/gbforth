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

function depth(gameboy) {
  return (((0xed - gameboy._cpu.c) / 2) | 0) + 1;
}

runTest(
  path.resolve(__dirname, "./test-dup.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 3);
    assert(memory[0xffe9] === 0x22);
    assert(memory[0xffeb] === 0x11);
    assert(gameboy._cpu.hl === 0x22);
  }
);

runTest(
  path.resolve(__dirname, "./test-swap.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 3);
    assert(memory[0xffe9] === 0x33);
    assert(memory[0xffeb] === 0x11);
    assert(gameboy._cpu.hl === 0x22);
  }
);

runTest(
  path.resolve(__dirname, "./test-drop.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 1);
    assert(gameboy._cpu.hl === 0x11);
  }
);

runTest(
  path.resolve(__dirname, "./test-memget.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 3);
    assert(memory[0xffe9] === 0xed);
    assert(memory[0xffeb] === 0xce);
    assert(gameboy._cpu.hl === 0x66);
  }
);

runTest(
  path.resolve(__dirname, "./test-memset.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 0);
    assert(memory[0x8501] === 0xce);
    assert(memory[0x8502] === 0xed);
    assert(memory[0x8503] === 0x66);
  }
);

runTest(
  path.resolve(__dirname, "./test-plus.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 1);
    assert(gameboy._cpu.hl === 0x33);
  }
);

runTest(
  path.resolve(__dirname, "./test-quadruple.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 1);
    assert(gameboy._cpu.hl === 0x44);
  }
);
