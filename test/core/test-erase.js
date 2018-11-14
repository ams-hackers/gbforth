const gb = require("../gbtest")(__filename);

test("erase", () => {
  gb.run();
  const dest = Array.from(gb.memory).slice(0xc101, 0xc109);
  expect(dest).toEqual([1, 2, 0, 0, 0, 6, 7, 8]);

  const [offset] = gb.stack;
  const rom = Array.from(gb.memory).slice(offset, offset + 10);
  expect(rom).toEqual([0, 0, 0, 0, 0, 0, 0, 0, 0, 0]);
});
