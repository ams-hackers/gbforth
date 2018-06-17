const gb = require("../gbtest")(__filename);

test("create", () => {
  gb.cycles(300);
  expect(gb.stack).toEqual([1122, 3344, 5566]);
});
