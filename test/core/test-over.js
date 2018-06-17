const gb = require("../gbtest")(__filename);

test("over", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([1111, 2222, 1111]);
});
