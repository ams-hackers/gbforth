const gb = require("../gbtest")(__filename);

const readCountedString = (addr) => {
  const len = gb.memory[addr]
  const chars = gb.memory.slice(addr + 1, addr + 1 + len)
  return String.fromCharCode(...chars)
}

test("plus-place", () => {
  gb.run();
  expect(gb.stack.length).toBe(1);
  expect(readCountedString(gb.stack[0])).toBe("Forth for GB")
});
