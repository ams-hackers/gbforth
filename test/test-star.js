const gb = require("./gbtest")(__filename);

test("star", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xad59]);
});
