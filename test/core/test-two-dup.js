const gb = require("../gbtest")(__filename);

test("two-dup", () => {
  gb.run();
  expect(gb.stack).toEqual([1111, 2222, 1111, 2222]);
});
