const gb = require("../gbtest")(__filename);

test("star", () => {
  gb.run();
  expect(gb.stack).toEqual([0xad59]);
});
