const gb = require("../gbtest")(__filename)

test("slash-string", () => {
  gb.run()

  expect(gb.depth).toBe(2)
  const addr = gb.stack[0]
  expect(gb.stack[1]).toBe(5)
  expect(gb.memory[addr]).toBe('W'.charCodeAt(0))
  expect(gb.memory[addr + 1]).toBe('o'.charCodeAt(0))
  expect(gb.memory[addr + 2]).toBe('r'.charCodeAt(0))
  expect(gb.memory[addr + 3]).toBe('l'.charCodeAt(0))
  expect(gb.memory[addr + 4]).toBe('d'.charCodeAt(0))
})
