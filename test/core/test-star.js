const gb = require("../gbtest")(__filename);

test("star", () => {
  gb.run();
  const i = x => 0xffff;
  expect(gb.stack).toEqual([0, 0xad59, 42, gb.int(-42)]);
});
