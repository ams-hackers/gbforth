const gb = require("../gbtest")(__filename);

test("not-rot", () => {
  gb.run();
  expect(gb.stack).toEqual([3333, 1111, 2222]);
});
