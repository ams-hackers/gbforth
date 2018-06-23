const gb = require("./gbtest")(__filename);

test("ahead...then", () => {
  gb.run();
  expect(gb.stack).toEqual([1, 2, 8, 9]);
});
