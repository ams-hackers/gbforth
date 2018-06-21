const gb = require("../gbtest")(__filename);

test("minus", () => {
  gb.run();
  expect(gb.stack).toEqual([0x9999, 0x0, 0xffff]);
});
