const gb = require("../gbtest")(__filename);

test("clearstack", () => {
  gb.run();
  expect(gb.stack).toEqual([4, 5, 6]);
});
