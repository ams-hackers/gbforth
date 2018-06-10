const gb = require("./gbtest")(__filename);

test("plus-store", () => {
  gb.cycles(200);

  expect(gb.depth).toBe(0);
  expect(gb.memory[0x8501]).toBe(0xcc);
  expect(gb.memory[0x8502]).toBe(0x99);
});
