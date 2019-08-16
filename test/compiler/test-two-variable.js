const gb = require("../gbtest")(__filename);

test("two-variable", () => {
  gb.run();
  expect(gb.stack).toEqual([0x1337, 0x42, 0x9292, 0x5252]);
});
