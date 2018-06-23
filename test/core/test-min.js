const gb = require("../gbtest")(__filename);

test("min", () => {
  gb.run();
  expect(gb.stack).toEqual([0x1234, 0x6789, 0xaa55, 0xaa22, gb.int(-1), gb.int(-2), gb.int(-11)]);
});
