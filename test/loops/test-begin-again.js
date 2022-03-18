const gb = require('../gbtest')(__filename)

test('begin...again', () => {
  gb.run({ maxFrames: 10 })
  expect(gb.stack).toHaveLength(1)
  expect(gb.stack[0]).toBeGreaterThan(10)
})
