const gb = require("../gbtest")(__filename);

test("or", () => {
  gb.run();
  expect(gb.stack).toEqual([0xffcd, 0xabff]);
});
