const gb = require("../gbtest")(__filename);

test("do...loop", () => {
  gb.run();
  expect(gb.stack).toEqual([3]);
});
