const gb = require("../gbtest")(__filename);

test("two-star", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x1234, 0xacf0]);
});
