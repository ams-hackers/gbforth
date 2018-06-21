const gb = require("../gbtest")(__filename);

test("drop", () => {
  gb.run();
  expect(gb.stack).toEqual([0x11]);
});
