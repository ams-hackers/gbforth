const gb = require("../gbtest")(__filename);

test("rstack", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([42, 43, 44]);
});
