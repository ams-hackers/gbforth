const gb = require("../gbtest")(__filename);

test("sliteral", () => {
  gb.run();
  expect(gb.stack.length).toEqual(2);
  expect(gb.stack[1]).toEqual(3);
  const addr = gb.stack[0]
  expect(gb.memory[addr]).toEqual('h'.charCodeAt(0))
  expect(gb.memory[addr + 1]).toEqual('o'.charCodeAt(0))
  expect(gb.memory[addr + 2]).toEqual('i'.charCodeAt(0))
});
