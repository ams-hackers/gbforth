const gb = require("./gbtest")(__filename);

test(":noname", () => {
  gb.cycles(200);
  expect(gb.stack).toEqual([10, 20, 30]);
});
