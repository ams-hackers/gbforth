const gb = require("../gbtest")(__filename);

test("two-swap", () => {
  gb.run();
  expect(gb.stack).toEqual([3333, 4444, 1111, 2222]);
});
