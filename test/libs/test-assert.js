const gb = require("../gbtest")(__filename);

test("Assert", () => {
  gb.run();
  expect(gb.stack).toEqual([
    0x03, 0x02, 0x01, 0x00,
    0x13, 0x12, 0x11, 0x10,
    0x23, 0x22, 0x21, 0x20,
    0x33
  ]);
});
