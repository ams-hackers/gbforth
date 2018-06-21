const gb = require("../gbtest")(__filename);

test("tuck", () => {
  gb.run();
  expect(gb.stack).toEqual([2222, 1111, 2222]);
});
