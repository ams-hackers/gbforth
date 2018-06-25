const gb = require("../gbtest")(__filename);

test("create", () => {
  gb.run();
  expect(gb.stack).toEqual([1221, 33, 44, 5566, gb.int(-1)]);
});
