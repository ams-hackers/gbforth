const gb = require("../gbtest")(__filename);

test("literal", () => {
  gb.run();
  expect(gb.stack).toEqual([0xa]);
});
