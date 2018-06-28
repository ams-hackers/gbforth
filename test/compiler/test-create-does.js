const gb = require("../gbtest")(__filename);

test("CREATE...DOES>", () => {
  gb.run();
  expect(gb.stack).toEqual([0x123, 0x5432]);
});
