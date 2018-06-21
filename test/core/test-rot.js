const gb = require("../gbtest")(__filename);

test("rot", () => {
  gb.run();
  expect(gb.stack).toEqual([2222, 3333, 1111]);
});
