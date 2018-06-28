const gb = require("../gbtest")(__filename);

test("rshift", () => {
  gb.run();
  expect(gb.stack).toEqual([0xab, 0xcd00, 0b0011111111111000]);
});
