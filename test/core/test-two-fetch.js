const gb = require("../gbtest")(__filename);

test("two-fetch", () => {
  gb.run();
  expect(gb.stack).toEqual([0x6666, 0xedce, 0x0b00, 0x0dcc]);
});
