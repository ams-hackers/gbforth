const gb = require("./gbtest")(__filename);

test("and", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xab00, 0xcd]);
});
