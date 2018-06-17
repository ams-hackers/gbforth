const gb = require("../gbtest")(__filename);

test("two-swap", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([3333, 4444, 1111, 2222]);
});
