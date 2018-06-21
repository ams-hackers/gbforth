const gb = require("../gbtest")(__filename);

test("fetch", () => {
  gb.run();
  expect(gb.stack).toEqual([0xedce, 0x6666]);
});
