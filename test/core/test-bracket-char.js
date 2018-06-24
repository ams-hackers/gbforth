const gb = require("../gbtest")(__filename);

test("bracket-char", () => {
  gb.run();
  expect(gb.stack).toEqual([65, 66]);
});
