const gb = require("./gbtest")(__filename);

test("begin...while...repeat", () => {
  gb.cycles(1000);
  expect(gb.stack).toEqual([20]);
});
