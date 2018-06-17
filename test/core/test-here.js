const gb = require("../gbtest")(__filename);

const DP0 = 0xC002;
const cell = 0x2;

test("here", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([DP0 + 3 * cell]);
});
