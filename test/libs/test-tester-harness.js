const gb = require("../gbtest")(__filename);

test("tester harness", () => {
  gb.run();
  expect(gb.stack).toEqual([2]);
});
