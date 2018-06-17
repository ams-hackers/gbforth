const gb = require("../gbtest")(__filename);

test("fetch", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xedce, 0x6666]);
});
