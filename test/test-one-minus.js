const gb = require("./gbtest")(__filename);

test("one plus", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0x8765, 0x4321]);
});
