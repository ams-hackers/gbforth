const gb = require('../gbtest')(__filename)

test('sliteral', () => {
  gb.run()
  expect(gb.stack).toEqual([116, 101, 115, 116])
})
