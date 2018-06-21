const gb = require("../gbtest")(__filename);

test("variable", () => {
  gb.run();
  expect(gb.stack).toEqual([0x42, 0x9001]);
});
