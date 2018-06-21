const gb = require("../gbtest")(__filename);

test("cfetch", () => {
  gb.run();
  expect(gb.stack).toEqual([0xce, 0xed, 0x66]);
});
