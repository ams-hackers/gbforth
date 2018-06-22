const gb = require("../gbtest")(__filename);

test("rstack", () => {
  gb.run();
  expect(gb.stack).toEqual([42, 43, 44, 45, 46, 47, 48, 49, 50]);
});
