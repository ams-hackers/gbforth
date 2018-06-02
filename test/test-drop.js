const gb = require("./gbtest")(__filename);

test("drop", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x11]);
});
