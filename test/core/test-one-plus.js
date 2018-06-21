const gb = require("../gbtest")(__filename);

test("one plus", () => {
  gb.run();
  expect(gb.stack).toEqual([0x1234, 0x5678]);
});
