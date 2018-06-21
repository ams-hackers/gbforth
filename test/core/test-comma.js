const gb = require("../gbtest")(__filename);

test("comma", () => {
  gb.run();
  const cp0 = gb.stack[0];
  const cp = gb.stack[1];
  expect(cp - cp0).toEqual(4);
  expect(gb.memory[cp0]).toBe(0xcd);
  expect(gb.memory[cp0 + 1]).toBe(0xab);
  expect(gb.memory[cp0 + 2]).toBe(0xef);
  expect(gb.memory[cp0 + 3]).toBe(0xcd);
});
