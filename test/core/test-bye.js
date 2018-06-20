const gb = require("../gbtest")(__filename);

test("bye", () => {
  gb.cycles(1000);
  expect(gb.stack).toEqual([42]);
});
