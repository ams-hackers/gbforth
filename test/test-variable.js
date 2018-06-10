const gb = require("./gbtest")(__filename);

test("variable", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x1337, 0x4242]);
});
