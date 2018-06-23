const gb = require("../gbtest")(__filename);

test("quit", () => {
  gb.run();
  expect(gb.stack).toEqual([42]);
});
