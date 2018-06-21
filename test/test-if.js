const gb = require("./gbtest")(__filename);

test("if", () => {
  gb.run();
  expect(gb.stack).toEqual([1, 2, 3, 0, 5, 5]);
});
