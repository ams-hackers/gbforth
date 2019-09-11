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

    int(n) {
      return n & 0xffff;
    },

    run(options = {}) {
      const HALT = 0x76;
      const STOP = 0x10;
      let cycles = 0;
      while (!options.maxCycles || cycles < options.maxCycles) {
        const pc = gameboy._cpu.pc;
        const bytecode = gameboy._mmu.readByte(pc);
        if (bytecode === HALT || bytecode === STOP) {
          break;
        }
        gameboy._cpu._runCycle();
        cycles++;
      }
    },

    input(code) {
      gameboy._joypad.keyDown(code);
    },

    K_LEFT: 37,
    K_UP: 38,
    K_RIGHT: 39,
    K_DOWN: 40,
    K_A: 90,
    K_B: 88,
    K_START: 13,
    K_SELECT: 16,

    saveFrame(filename) {
      fs.writeFileSync(filename, frame);
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

      // HL is TOS only if the stack is no empty
      if (c < SP0) {
        stack.push(gameboy._cpu.hl);
      }

      for (let i = c; i < SP0 - 2; i += 2) {
        stack.push(gameboy._mmu.readWord(0xff00 + i));
      }

      return stack.reverse();
    }
  };
};
