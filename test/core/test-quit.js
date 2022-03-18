const gb = require('../gbtest')(__filename)

test('quit', () => {
  gb.run({ maxFrames: 10 })
  expect(gb.stack).toEqual([42])
})
