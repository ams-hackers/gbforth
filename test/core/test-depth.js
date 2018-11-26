const gb = require("../gbtest")(__filename);

test("depth", () => {
  gb.run();
  expect(gb.stack).toEqual([0, 1, 2, 3, 4, 5]);
});
