const gb = require("../gbtest")(__filename);

test("nip", () => {
  gb.run();
  expect(gb.stack).toEqual([2222]);
});
