const gb = require("../gbtest")(__filename)

test("count", () => {
  gb.run()

  expect(gb.depth).toBe(3)
  expect(gb.stack[2]).toBe(5)
  expect(gb.stack[1]).toBe(gb.stack[0] + 1)
})
