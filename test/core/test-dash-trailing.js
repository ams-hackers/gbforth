const gb = require("../gbtest")(__filename)

const readString = (addr, len) => {
  const chars = gb.memory.slice(addr, addr + len)
  return String.fromCharCode(...chars)
}

test("dash-trailing", () => {
  gb.run()

  expect(gb.depth).toBe(10)
  expect(readString(gb.stack[8], gb.stack[9])).toBe("")
  expect(readString(gb.stack[6], gb.stack[7])).toBe("Hello World")
  expect(readString(gb.stack[4], gb.stack[5])).toBe("  Hello World")
  expect(readString(gb.stack[2], gb.stack[3])).toBe("   Hello World")
  expect(readString(gb.stack[0], gb.stack[1])).toBe("")
})
