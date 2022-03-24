const gb = require("../gbtest")(__filename);

test("tick", () => {
  gb.run();
  expect(gb.stack.length).toEqual(5)
  expect(gb.stack[0]).toEqual(gb.stack[1]);
  expect(gb.stack[0]).toEqual(gb.stack[2]);
  expect(gb.stack[0]).toEqual(gb.stack[3]);
  expect(gb.stack[0]).toEqual(gb.stack[4]);
});
