const gb = require("../gbtest")(__filename);

test(":noname", () => {
  gb.run();
  expect(gb.stack).toEqual([10, 20, 30]);
});
