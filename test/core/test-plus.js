const gb = require("../gbtest")(__filename);

test("plus", () => {
  gb.run();
  expect(gb.stack).toEqual([0xabcd]);
});
