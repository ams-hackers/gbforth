const gb = require("../gbtest")(__filename);

test("over", () => {
  gb.run();
  expect(gb.stack).toEqual([1111, 2222, 1111]);
});
