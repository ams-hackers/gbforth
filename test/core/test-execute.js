const gb = require("../gbtest")(__filename);

test("execute", () => {
  gb.run();
  expect(gb.stack).toEqual([0x11]);
});
