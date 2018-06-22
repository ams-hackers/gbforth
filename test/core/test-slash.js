const gb = require("../gbtest")(__filename);

test("slash", () => {
  gb.run();
  expect(gb.stack).toEqual([
    4,
    3,
    4,
    gb.int(-5),
    gb.int(-5),
    5,
    gb.int(-4),
    3,
    0
  ]);
});
