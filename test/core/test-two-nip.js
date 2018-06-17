const gb = require("../gbtest")(__filename);

test("two-nip", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([3333, 4444]);
});
