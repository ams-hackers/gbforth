const gb = require("../gbtest")(__filename);

test("two-nip", () => {
  gb.run();
  expect(gb.stack).toEqual([3333, 4444]);
});
