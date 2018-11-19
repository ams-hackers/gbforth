const gb = require("../gbtest")(__filename);

test("exit", () => {
  gb.run();
  expect(gb.stack).toEqual([42, 43, 44]);
});
