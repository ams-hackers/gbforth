const gb = require("../gbtest")(__filename);

test("slash", () => {
  gb.run();
  expect(gb.stack).toEqual([4, 3, 4]);
});
