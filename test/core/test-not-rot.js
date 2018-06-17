const gb = require("../gbtest")(__filename);

test("not-rot", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([3333, 1111, 2222]);
});
