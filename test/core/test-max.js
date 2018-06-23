const gb = require("../gbtest")(__filename);

test("max", () => {
  gb.run();
  expect(gb.stack).toEqual([0x5678, 0xabcd, 0xaa88, 0xaa44, 0x0, gb.int(-2), gb.int(-10)]);
});
