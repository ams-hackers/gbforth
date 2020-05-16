const gb = require("../gbtest")(__filename);

test("do...loop I J K", () => {
  gb.run();
  expect(gb.stack).toEqual([7, 9, 10, 13, 8, 10, 11, 14]);
});
