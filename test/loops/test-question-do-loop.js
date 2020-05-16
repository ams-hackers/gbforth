const gb = require("../gbtest")(__filename);

test("?do...loop", () => {
  gb.run();
  const result1 = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 99];
  const result2 = [0, 1, 2];
  const result3 = [0, 1, 2, 99];
  const result4 = [42];
  expect(gb.stack).toEqual([...result1, ...result2, ...result3, ...result4]);
});
