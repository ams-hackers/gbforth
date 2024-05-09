const gb = require('../gbtest')(__filename)

test('bracket-if', () => {
  gb.run()
  expect(gb.stack).toEqual([1, 3, 6])
})
