const gb = require("../gbtest")(__filename)

test("bounds", () => {
  gb.run()

  expect(gb.depth).toBe(5)
  expect(gb.stack).toEqual([
    'H'.charCodeAt(0),
    'e'.charCodeAt(0),
    'l'.charCodeAt(0),
    'l'.charCodeAt(0),
    'o'.charCodeAt(0),
  ])
})
