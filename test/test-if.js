const gb = require("./gbtest")(__filename);

test("if", () => {
  gb.cycles(400);
  expect(gb.stack).toEqual([1, 2, 3]);
});
