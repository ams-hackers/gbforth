"use strict";

const assert = require("assert");
const path = require("path");
const fs = require("fs");
const crypto = require("crypto");
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

function getSha(x) {
  const hash = crypto.createHash("sha256");
  hash.update(x);
  return hash.digest("hex");
}

function runVisualTest(rompath, { steps }, cb) {
  const gameboy = new Gameboy();
  gameboy.loadCart(fs.readFileSync(rompath));

  let imgBuffer;
  gameboy.gpu.on("frame", canvas => {
    imgBuffer = canvas.toBuffer();
  });

  gameboy._init();
  for (let i = 0; i < steps; i++) {
    gameboy._cpu._step();
  }

  const sha = getSha(imgBuffer);
  cb(imgBuffer, sha);
}

const SP0 = 0xFE

function depth(gameboy) {
  return (((SP0 - 2 - gameboy._cpu.c) / 2) | 0) + 1;
}

function stack(gameboy) {
  const c = gameboy._cpu.c;
  const stack = [];
  for (let i = c; i < SP0 - 2; i += 2) {
    stack.push(gameboy._mmu.readWord(0xff00 + i));
  }
  // HL is TOS only if the stack is no empty
  if (c < SP0) {
    stack.push(gameboy._cpu.hl);
  }

  return stack.reverse();
}

runTest(
  path.resolve(__dirname, "./test-asm-add.gb"),
  { cycles: 200 },
  gameboy => {
    assert.equal(gameboy._cpu.a, 0x33);
    assert.equal(gameboy._cpu.d, 0x33);
    assert.equal(gameboy._cpu.e, 0x33);
  }
);

runTest(
  path.resolve(__dirname, "./test-asm-sub.gb"),
  { cycles: 200 },
  gameboy => {
    assert.equal(gameboy._cpu.a, 0x11);
    assert.equal(gameboy._cpu.d, 0x11);
    assert.equal(gameboy._cpu.e, 0x11);
  }
);

runTest(path.resolve(__dirname, "./test-dup.gb"), { cycles: 200 }, gameboy => {
  assert.deepStrictEqual(stack(gameboy), [0x22, 0x11, 0x22]);
});

runTest(path.resolve(__dirname, "./test-swap.gb"), { cycles: 200 }, gameboy => {
  assert.deepStrictEqual(stack(gameboy), [0x22, 0x11, 0x33]);
});

runTest(path.resolve(__dirname, "./test-drop.gb"), { cycles: 200 }, gameboy => {
  assert.deepStrictEqual(stack(gameboy), [0x11]);
});

runTest(
  path.resolve(__dirname, "./test-cfetch.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0x66, 0xce, 0xed]);
  }
);

runTest(
  path.resolve(__dirname, "./test-cstore.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 0);
    assert(memory[0x8501] === 0xce);
    assert(memory[0x8502] === 0xed);
    assert(memory[0x8503] === 0x66);
  }
);

runTest(
  path.resolve(__dirname, "./test-fetch.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0x6666, 0xceed]);
  }
);

runTest(
  path.resolve(__dirname, "./test-store.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert(depth(gameboy) === 0);
    assert(memory[0x8501] === 0xab);
    assert(memory[0x8502] === 0xcd);
    assert(memory[0x8503] === 0x12);
    assert(memory[0x8504] === 0x34);
  }
);

runTest(
  path.resolve(__dirname, "./test-plus.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0x33]);
  }
);

runTest(
  path.resolve(__dirname, "./test-star.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0xad59]);
  }
);

runTest(
  path.resolve(__dirname, "./test-double.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0x44]);
  }
);

runTest(
  path.resolve(__dirname, "./test-colon-shadow.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [11]);
  }
);

runTest(
  path.resolve(__dirname, "./test-execute.gb"),
  { cycles: 200 },
  (gameboy, memory) => {
    assert.deepStrictEqual(stack(gameboy), [0x11]);
  }
);

runTest(
  path.resolve(__dirname, "./test-cmove.gb"),
  { cycles: 1000 },
  (gameboy, memory) => {
    const dest = Array.from(memory).slice(0xc201, 0xc207);
    assert.deepStrictEqual(dest, [1, 2, 3, 4, 5, 0]);
    assert.deepStrictEqual(stack(gameboy), []);
  }
);

runVisualTest(
  path.resolve(__dirname, "../examples/hello-world-asm/hello.gb"),
  { steps: 10 },
  (imgBuffer, sha) => {
    // fs.writeFileSync(`hello-asm.png`, imgBuffer);
    assert.equal(
      sha,
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  }
);

runVisualTest(
  path.resolve(__dirname, "../examples/hello-world/hello.gb"),
  { steps: 10 },
  (imgBuffer, sha) => {
    // fs.writeFileSync(`hello-forth.png`, imgBuffer);
    assert.equal(
      sha,
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  }
);
