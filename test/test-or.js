const gb = require("./gbtest")(__filename);

test("or", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([0xffcd, 0xabff]);
});
