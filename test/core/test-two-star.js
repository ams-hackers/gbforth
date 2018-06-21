const gb = require("../gbtest")(__filename);

test("two-star", () => {
  gb.run();
  expect(gb.stack).toEqual([0x1234, 0xacf0]);
});
