const gb = require("../gbtest")(__filename);

test("invert", () => {
  gb.run();
  expect(gb.stack).toEqual([0xffff, 0x0000, 0xf00f]);
});
