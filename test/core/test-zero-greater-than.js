const gb = require("../gbtest")(__filename);

test("zero-greater-than", () => {
  gb.run();
  expect(gb.stack).toEqual([0x0000, 0x0000, 0xffff]);
});
