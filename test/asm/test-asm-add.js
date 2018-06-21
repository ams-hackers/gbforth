const gb = require("../gbtest")(__filename);

test("test-asm-add", () => {
  gb.run();
  expect(gb.gameboy._cpu.a).toBe(0x33);
  expect(gb.gameboy._cpu.d).toBe(0x33);
  expect(gb.gameboy._cpu.e).toBe(0x33);
});
