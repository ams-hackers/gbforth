const gb = require("../gbtest")(__filename);

test("two-drop", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([1111]);
});
