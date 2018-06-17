const gb = require("../gbtest")(__filename);

test("minus", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x9999, 0x0, 0xFFFF]);
});
