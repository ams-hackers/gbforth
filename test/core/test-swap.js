const gb = require("../gbtest")(__filename);

test("swap", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x11, 0x33, 0x22]);
});
