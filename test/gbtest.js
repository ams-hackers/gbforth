const fs = require("fs");
const Gameboy = require("node-gameboy");
const crypto = require("crypto");

const SP0 = 0xfe;

function getSha(x) {
  const hash = crypto.createHash("sha256");
  hash.update(x);
  return hash.digest("hex");
}

module.exports = filename => {
  const rompath = filename.replace(/.js$/, ".gb");

  let gameboy;
  let frame;

  beforeEach(() => {
    gameboy = new Gameboy();
    gameboy.loadCart(fs.readFileSync(rompath));
    gameboy._init();

    gameboy.gpu.on("frame", canvas => {
      frame = canvas.toBuffer();
    });
  });

  return {
    get gameboy() {
      return gameboy;
    },

    steps(n) {
      for (let i = 0; i < n; i++) {
        gameboy._cpu._step();
      }
    },

    cycles(n) {
      for (let i = 0; i < n; i++) {
        gameboy._cpu._runCycle();
      }
    },

    get frameSha() {
      return getSha(frame);
    },

    get depth() {
      return (((SP0 - 2 - gameboy._cpu.c) / 2) | 0) + 1;
    },

    get memory() {
      const memory = new Uint8Array(65536);
      for (let offset = 0; offset <= 0xffff; offset++) {
        memory[offset] = gameboy._mmu.readByte(offset);
      }
      return memory;
    },

    get stack() {
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
  };
};
