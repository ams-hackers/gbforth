const gb = require("./gbtest")(__filename);

test("double", () => {
  gb.run();
  expect(gb.stack).toEqual([0x44]);
});
