const gb = require("../gbtest")(__filename);

test("xor", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xfdb5]);
});
