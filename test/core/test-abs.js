const gb = require("../gbtest")(__filename);

const wrap = x => 65536 - x;

test("and", () => {
  gb.run();
  expect(gb.stack).toEqual([23456, 12345, 0, wrap(45678), wrap(56789)]);
});
