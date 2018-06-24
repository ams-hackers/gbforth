const gb = require("./gbtest")(__filename);

test("?do...+loop", () => {
  gb.run();
  const result1 = [0, 3, 6, 9];
  const result2 = [0, gb.int(-1)];
  expect(gb.stack).toEqual([...result1, ...result2]);
});
