const gb = require("../gbtest")(__filename);

test("swap", () => {
  gb.run();
  expect(gb.stack).toEqual([0x11, 0x33, 0x22]);
});
