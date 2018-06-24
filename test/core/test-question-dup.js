const gb = require("../gbtest")(__filename);

test("?dup", () => {
  gb.run();
  expect(gb.stack).toEqual([12, 12, gb.int(-1), gb.int(-1), 0]);
});
