const gb = require("../gbtest")(__filename);

test("one minus", () => {
  gb.run();
  expect(gb.stack).toEqual([0x8765, 0x4321, gb.int(-1234)]);
});
