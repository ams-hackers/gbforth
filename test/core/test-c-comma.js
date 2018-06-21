const gb = require("../gbtest")(__filename);

test("c-comma", () => {
  gb.run();
  const cp0 = gb.stack[0];
  const cp = gb.stack[1];
  expect(cp - cp0).toEqual(2);
  expect(gb.memory[cp0]).toBe(0xef);
  expect(gb.memory[cp0 + 1]).toBe(0x42);
});
