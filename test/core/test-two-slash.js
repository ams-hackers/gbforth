const gb = require("../gbtest")(__filename);

test("two-slash", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x1234, 0x2b3c]);
});
