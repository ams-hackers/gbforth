const gb = require("../gbtest")(__filename);

test("DEFER...IS", () => {
  gb.run();
  expect(gb.stack).toEqual([43, 41]);
});
