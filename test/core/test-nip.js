const gb = require("../gbtest")(__filename);

test("nip", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([2222]);
});
