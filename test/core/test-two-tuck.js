const gb = require("../gbtest")(__filename);

test("two-tuck", () => {
  gb.cycles(300);
  expect(gb.stack).toEqual([3333, 4444, 1111, 2222, 3333, 4444]);
});
