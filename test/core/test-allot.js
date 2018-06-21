const gb = require("../gbtest")(__filename);

test("allot", () => {
  gb.run();
  expect(gb.stack[1] - gb.stack[0]).toEqual(0x42);
});
