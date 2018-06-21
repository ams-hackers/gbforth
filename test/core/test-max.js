const gb = require("../gbtest")(__filename);

test("max", () => {
  gb.run();
  expect(gb.stack).toEqual([0x5678, 0xabcd, 0xaa88, 0xaa44]);
});
