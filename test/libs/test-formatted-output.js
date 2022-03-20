const gb = require("../gbtest")(__filename);

const readCountedString = (addr) => {
  const len = gb.memory[addr]
  const chars = gb.memory.slice(addr + 1, addr + 1 + len)
  return String.fromCharCode(...chars)

}

test("formatted output", () => {
  gb.run();
  expect(gb.stack.length).toBe(9);
  expect(readCountedString(gb.stack[0])).toBe("¥0.04")
  expect(readCountedString(gb.stack[1])).toBe("¥0.21")
  expect(readCountedString(gb.stack[2])).toBe("¥19.89")
  expect(readCountedString(gb.stack[3])).toBe("21500")
  expect(readCountedString(gb.stack[4])).toBe("0")
  expect(readCountedString(gb.stack[5])).toBe("(21500)")
  expect(readCountedString(gb.stack[6])).toBe("9876")
  expect(readCountedString(gb.stack[7])).toBe("0")
  expect(readCountedString(gb.stack[8])).toBe("-1234")
});
