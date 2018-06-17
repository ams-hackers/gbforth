const gb = require("../gbtest")(__filename);

test("colon-shadow", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([11]);
});
