const gb = require("../gbtest")(__filename);

test("store", () => {
  gb.run();
  gb.run();

  expect(gb.depth).toBe(0);
  expect(gb.memory[0x8501]).toBe(0xcd);
  expect(gb.memory[0x8502]).toBe(0xab);
  expect(gb.memory[0x8503]).toBe(0x34);
  expect(gb.memory[0x8504]).toBe(0x12);
});
