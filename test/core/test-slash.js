const gb = require("../gbtest")(__filename);

test("slash", () => {
  gb.cycles(300);
  expect(gb.stack).toEqual([4, 3, 4]);
});
