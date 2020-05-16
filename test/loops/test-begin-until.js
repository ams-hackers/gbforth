const gb = require("../gbtest")(__filename);

test("begin...until", () => {
  gb.run();
  expect(gb.stack).toEqual([1, 20]);
});
