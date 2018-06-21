const gb = require("./gbtest")(__filename);

test("pick", () => {
  gb.run();
  expect(gb.stack).toEqual([0, 4]);
});
