const gb = require("../gbtest")(__filename);

test("two-rot", () => {
  gb.cycles(400);
  expect(gb.stack).toEqual([3333, 4444, 5555, 6666, 1111, 2222]);
});
