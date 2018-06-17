const gb = require("../gbtest")(__filename);

test("cfetch", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xce, 0xed, 0x66]);
});
