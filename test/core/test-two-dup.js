const gb = require("../gbtest")(__filename);

test("two-dup", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([1111, 2222, 1111, 2222]);
});
