const gb = require("../gbtest")(__filename);

test("slash-mod", () => {
  gb.run();
  expect(gb.stack).toEqual([
    gb.int(-3),
    gb.int(1),

    gb.int(-4),
    gb.int(2),

    gb.int(0),
    gb.int(0),

    gb.int(0),
    gb.int(0)
  ]);
});
