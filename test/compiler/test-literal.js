const gb = require("../gbtest")(__filename);

test("literal", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xA]);
});
