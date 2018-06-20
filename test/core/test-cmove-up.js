const gb = require("../gbtest")(__filename);

test("cmove-up", () => {
  gb.cycles(1000);
  const dest = Array.from(gb.memory).slice(0xc100, 0xc109);
  expect(dest).toEqual([0, 1, 2, 1, 2, 3, 4, 5, 0]);
  expect(gb.depth).toBe(0);
});
