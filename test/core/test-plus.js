const gb = require("../gbtest")(__filename);

test("plus", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xabcd]);
});
