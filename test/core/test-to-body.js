const gb = require("../gbtest")(__filename);

test("to-body", () => {
  gb.run();
  expect(gb.stack).toEqual([0xFFFF, 0xFFFF, 0xFFFF]);
});
