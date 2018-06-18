const gb = require("./gbtest")(__filename);

test("begin...again", () => {
  gb.cycles(200);
  expect(gb.stack).toHaveLength(1);
  expect(gb.stack[0]).toBeGreaterThan(10);
});
