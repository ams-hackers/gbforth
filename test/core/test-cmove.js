const gb = require("../gbtest")(__filename);

test("cmove", () => {
  gb.cycles(1000);
  const dest = Array.from(gb.memory).slice(0xc0fe, 0xc107);
  expect(dest).toEqual([0, 1, 2, 3, 4, 5, 4, 5, 6]);
  expect(gb.depth).toBe(0);
});
