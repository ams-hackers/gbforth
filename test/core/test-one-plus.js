const gb = require("../gbtest")(__filename);

test("one plus", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x1234, 0x5678]);
});
