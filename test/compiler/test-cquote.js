const gb = require("../gbtest")(__filename);

const readCountedString = (addr) => {
  const len = gb.memory[addr]
  const chars = gb.memory.slice(addr + 1, addr + 1 + len)
  return String.fromCharCode(...chars)
}

test("c-quote", () => {
  gb.run();
  expect(gb.stack.length).toEqual(2);
  expect(readCountedString(gb.stack[1])).toEqual("world");
  expect(readCountedString(gb.stack[0])).toEqual("Hi!");
});
