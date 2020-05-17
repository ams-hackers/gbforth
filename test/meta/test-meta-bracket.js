const gb = require("../gbtest")(__filename);

test("meta-bracket", () => {
  gb.run();
  expect(gb.stack).toEqual([42]);
});
