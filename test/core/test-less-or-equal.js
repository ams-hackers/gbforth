const gb = require("../gbtest")(__filename);

test("less-or-equal", () => {
  gb.run();
  expect(gb.stack).toEqual([0x0, 0xffff, 0xffff, 0xffff]);
});
