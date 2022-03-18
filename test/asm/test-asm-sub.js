const gb = require("../gbtest")(__filename);

test("test-asm-sub", () => {
  gb.run();
  expect(gb.registers.a).toBe(0x11);
  expect(gb.registers.d).toBe(0x11);
  expect(gb.registers.e).toBe(0x11);
});
