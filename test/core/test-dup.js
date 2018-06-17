const gb = require("../gbtest")(__filename);

test("dup", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x11, 0x22, 0x22]);
});
