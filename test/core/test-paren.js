const gb = require("../gbtest")(__filename);

test("paren", () => {
  gb.run();
  expect(gb.stack).toEqual([2000, 5000]);
});
