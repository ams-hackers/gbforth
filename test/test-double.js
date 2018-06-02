const gb = require("./gbtest")(__filename);

test("double", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x44]);
});
