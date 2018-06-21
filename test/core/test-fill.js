const gb = require("../gbtest")(__filename);

test("fill", () => {
  gb.run();
  const dest = Array.from(gb.memory).slice(0xc101, 0xc109);
  expect(dest).toEqual([1, 2, 42, 42, 42, 6, 7, 8]);
  expect(gb.depth).toBe(0);
});
