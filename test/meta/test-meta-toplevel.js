const gb = require("../gbtest")(__filename);

test("meta-toplevel", () => {
  gb.run();
  expect(gb.stack).toEqual([64]);
});
