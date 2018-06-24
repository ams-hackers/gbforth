const gb = require("./gbtest")(__filename);

test("Bits", () => {
  gb.run();
  expect(gb.stack).toEqual([4, 8, 1, 0]);
});
