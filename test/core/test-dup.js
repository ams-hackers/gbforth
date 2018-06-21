const gb = require("../gbtest")(__filename);

test("dup", () => {
  gb.run();
  expect(gb.stack).toEqual([0x11, 0x22, 0x22]);
});
