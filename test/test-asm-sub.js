const gb = require("./gbtest")(__filename);

test("test-asm-sub", () => {
  gb.cycles(200);
  expect(gb.gameboy._cpu.a).toBe(0x11);
  expect(gb.gameboy._cpu.d).toBe(0x11);
  expect(gb.gameboy._cpu.e).toBe(0x11);
});
