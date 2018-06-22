const gb = require("./gbtest")(__filename);

test("?do...loop", () => {
  gb.run();
  expect(gb.stack).toEqual([0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 99, 100, 101]);
});
