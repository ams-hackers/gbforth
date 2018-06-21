const gb = require("../gbtest")(__filename);

test("greater-than", () => {
  gb.run();
  expect(gb.stack).toEqual([0x0000, 0xffff, 0x0000]);
});
