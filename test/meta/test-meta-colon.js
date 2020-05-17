const gb = require("../gbtest")(__filename);

test("meta-colon", () => {
  gb.run();
  expect(gb.stack).toEqual([50]);
});
