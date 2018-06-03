const gb = require("./gbtest")(__filename);

test("rshift", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xab, 0xcd00]);
});
