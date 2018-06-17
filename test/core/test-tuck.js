const gb = require("../gbtest")(__filename);

test("tuck", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([2222, 1111, 2222]);
});
