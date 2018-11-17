const gb = require("../gbtest")(__filename);

test("two-slash", () => {
  gb.run();
  expect(gb.stack).toEqual([0x1234, 0x2b3c, gb.int(-8), gb.int(-7), 6]);
});
