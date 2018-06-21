const gb = require("../gbtest")(__filename);

test("bye", () => {
  gb.run();
  expect(gb.stack).toEqual([42]);
});
