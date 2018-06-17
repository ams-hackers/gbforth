const gb = require("../gbtest")(__filename);

test("cstore", () => {
  gb.cycles(200);

  expect(gb.depth).toBe(0);
  expect(gb.memory[0x8501]).toBe(0xce);
  expect(gb.memory[0x8502]).toBe(0xed);
  expect(gb.memory[0x8503]).toBe(0x66);
});
