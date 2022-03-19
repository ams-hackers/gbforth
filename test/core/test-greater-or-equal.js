const gb = require("../gbtest")(__filename);

test("greater-or-equal", () => {
  gb.run();
  expect(gb.stack).toEqual([ 0xffff, 0xffff, 0x0, 0xffff]);
});
