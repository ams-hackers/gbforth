const gb = require("../gbtest")(__filename);

test("xor", () => {
  gb.run();
  expect(gb.stack).toEqual([0xfdb5]);
});
