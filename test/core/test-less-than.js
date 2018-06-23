const gb = require("../gbtest")(__filename);

test("less-than", () => {
  gb.run();
  expect(gb.stack).toEqual([0x0, 0xffff, 0x0, 0xffff, 0x0, 0x0, 0xffff, 0x0]);
});
