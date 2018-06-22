const gb = require("../gbtest")(__filename);

test("zero-less-than", () => {
  gb.run();
  expect(gb.stack).toEqual([0xffff, 0x0000, 0x0000]);
});
