const gb = require("../gbtest")(__filename);

test("equals", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x0, 0x0, 0x0, 0x0, 0xFFFF]);
});
