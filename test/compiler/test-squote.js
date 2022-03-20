const gb = require("../gbtest")(__filename);

const readString = (addr, len) => {
  const chars = gb.memory.slice(addr, addr + len)
  return String.fromCharCode(...chars)
}

test("s-quote", () => {
  gb.run();
  expect(gb.stack.length).toEqual(4);
  expect(readString(gb.stack[2], gb.stack[3])).toEqual("world");
  expect(readString(gb.stack[0], gb.stack[1])).toEqual("Hi!");
});
