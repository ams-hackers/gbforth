const Gameboy = require('serverboy')
const fs = require('fs')

const crypto = require('crypto')
function getSha(x) {
  const hash = crypto.createHash('sha256')
  hash.update(x)
  return hash.digest('hex')
}

const SP0 = 0xfe

module.exports = (filename) => {
  const rompath = filename.replace(/.js$/, '.gb')
  const rom = fs.readFileSync(rompath)

  const gameboy = new Gameboy()
  gameboy.loadRom(rom)

  const cpu = Object.values(gameboy)[0].gameboy

  return {
    steps(n) {
      for (let i = 0; i < n; i++) {
        gameboy.doFrame()
      }
    },
    int(n) {
      return n & 0xffff
    },
    run(options = { maxFrames: Infinity }) {
      let frame = 0
      do {
        gameboy.doFrame()
      } while (!cpu.halt && frame++ < options.maxFrames)
    },
    input(code) {
      gameboy.pressKey(code)
    },
    K_RIGHT: 0,
    K_LEFT: 1,
    K_UP: 2,
    K_DOWN: 3,
    K_A: 4,
    K_B: 5,
    K_SELECT: 6,
    K_START: 7,
    // get frameSha() {
    //   return saveFrame()
    //   gameboy.doFrame()
    //   const lcd = gameboy.getScreen()
    //
    //   return getSha(new Buffer(lcd))
    // },
    get frameSha() {
      gameboy.doFrame()
      const lcd = gameboy.getScreen()
      const html = `<!DOCTYPE html>
      <html>
        <body>
          <canvas></canvas>
          <script type="text/javascript">
            const lcd = [${lcd.join(',')}]
            const canvas = document.querySelector('canvas')
            canvas.width = 160
            canvas.height = 144
            canvas.style.width = '320px'
            canvas.style['image-rendering'] = 'pixelated'
            const ctx = canvas.getContext('2d')
            const imgData = ctx.createImageData(160, 144)
            for (let i = 0; i < lcd.length; i++) {
              imgData.data[i] = lcd[i]
            }
            ctx.putImageData(imgData, 0, 0)
          </script>
        </body>
      </html>`
      const sha = getSha(new Buffer(lcd))
      fs.writeFileSync(`${sha}.html`, html)
      return sha
    },
    get depth() {
      return (((SP0 - 2 - cpu.registerC) / 2) | 0) + 1
    },
    get registers() {
      return {
        a: cpu.registerA,
        b: cpu.registerB,
        c: cpu.registerC,
        d: cpu.registerD,
        e: cpu.registerE,
        f: cpu.registerF,
        hl: cpu.registersHL,
      }
    },
    get memory() {
      return gameboy.getMemory()
    },
    get stack() {
      const c = cpu.registerC
      const stack = []

      // HL is TOS only if the stack is no empty
      if (c < SP0) {
        stack.push(cpu.registersHL)
      }

      const mem = gameboy.getMemory()
      for (let i = c; i < SP0 - 2; i += 2) {
        const lo = mem[0xff00 + i]
        const hi = mem[0xff00 + i + 1]
        stack.push((hi << 8) + lo)
      }

      return stack.reverse()
    },
  }
}
