const gb = require("./gbtest")(__filename);

test("min", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x1234, 0x6789, 0xaa55, 0xaa22]);
});
