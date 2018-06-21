const gb = require("../gbtest")(__filename);

test("u-less-than", () => {
  gb.run();
  expect(gb.stack).toEqual([0xffff, 0x0000]);
});
