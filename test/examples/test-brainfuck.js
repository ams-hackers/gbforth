const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/brainfuck/brainfuck.gb")
);

describe("Brainfuck", () => {
  test('Outputs "Hello World!"', () => {
    gb.run();
    expect(gb.frameSha).toBe(
      "61086ce6ce247baa24a1c17d0f36cf70e05fffcc0fca33d8e289b501fe93f244"
    );
  });
});
