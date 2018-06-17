const gb = require("../gbtest")(__filename);

test("cmove", () => {
  gb.cycles(1000);
  const dest = Array.from(gb.memory).slice(0xc201, 0xc207);
  expect(dest).toEqual([1, 2, 3, 4, 5, 0]);
  expect(gb.depth).toBe(0);
});
