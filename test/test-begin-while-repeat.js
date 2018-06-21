const gb = require("./gbtest")(__filename);

test("begin...while...repeat", () => {
  gb.run();
  expect(gb.stack).toEqual([20]);
});
