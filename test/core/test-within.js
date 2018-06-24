const gb = require("../gbtest")(__filename);

test("within", () => {
  gb.run();
  expect(gb.stack).toEqual([
    gb.int(-1),
    gb.int(0),
    gb.int(-1),
    gb.int(-1),
    gb.int(-1),
    gb.int(0),
    gb.int(-1)
  ]);
});
