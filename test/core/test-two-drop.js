const gb = require("../gbtest")(__filename);

test("two-drop", () => {
  gb.run();
  expect(gb.stack).toEqual([1111]);
});
