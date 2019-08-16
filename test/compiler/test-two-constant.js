const gb = require("../gbtest")(__filename);

test("two-constant", () => {
  gb.run();
  expect(gb.stack).toEqual([0x150, 0x5, 0x42, 0x9001]);
});
