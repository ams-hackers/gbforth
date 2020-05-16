const gb = require("../gbtest")(__filename);

test("colon-basic", () => {
  gb.run();
  expect(gb.stack).toEqual([0x44]);
});
