const gb = require("../gbtest")(__filename);

const DP0 = 0xc002;
const cell = 0x2;

test("here", () => {
  gb.run();
  expect(gb.stack).toEqual([DP0 + 3 * cell]);
});
