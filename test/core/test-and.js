const gb = require("../gbtest")(__filename);

test("and", () => {
  gb.run();
  expect(gb.stack).toEqual([0xab00, 0xcd]);
});
