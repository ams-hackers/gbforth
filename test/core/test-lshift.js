const gb = require("../gbtest")(__filename);

test("lshift", () => {
  gb.run();
  expect(gb.stack).toEqual([0xab00, 0xcd, gb.int(-64)]);
});
