const gb = require("../gbtest")(__filename);

test("VALUE...TO", () => {
  gb.run();
  expect(gb.stack).toEqual([84, 71]);
});
