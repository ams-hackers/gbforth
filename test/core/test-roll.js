const gb = require("../gbtest")(__filename);

test("roll", () => {
  gb.run();
  expect(gb.stack).toEqual([0x02, 0x01, 0x03]);
});
