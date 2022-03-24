const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/brainfuck/brainfuck.gb")
);

describe("Brainfuck", () => {
  test('Outputs "Hello World!"', () => {
    gb.steps(80);
    expect(gb.frameSha).toBe(
      "a73773c2a18aa09afa0e2c78737b857fbe57ffc30cca043b66c0883e56d31a0b"
    );
  });
});
