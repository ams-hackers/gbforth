const gb = require("../gbtest")(__filename)

test("place", () => {
  gb.run()

  expect(gb.depth).toBe(1)

  const addr = gb.stack[0]
  expect(gb.memory[addr]).toBe(5)
  expect(gb.memory[addr + 1]).toBe('H'.charCodeAt(0))
  expect(gb.memory[addr + 2]).toBe('e'.charCodeAt(0))
  expect(gb.memory[addr + 3]).toBe('l'.charCodeAt(0))
  expect(gb.memory[addr + 4]).toBe('l'.charCodeAt(0))
  expect(gb.memory[addr + 5]).toBe('o'.charCodeAt(0))
})
