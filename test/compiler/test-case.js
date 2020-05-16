const gb = require("../gbtest")(__filename);

test("case", () => {
  gb.run();
  expect(gb.stack).toEqual([10, 20, 30, 100]);
});
