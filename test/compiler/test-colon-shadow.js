const gb = require("../gbtest")(__filename);

test("colon-shadow", () => {
  gb.run();
  expect(gb.stack).toEqual([11]);
});
