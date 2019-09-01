const path = require("path");

describe("hello-world-asm", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/hello-world-asm/hello.gb")
  );

  test('Shows "Hello World !"', () => {
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  });
});

describe("hello-world", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/hello-world/hello.gb")
  );

  test('Shows "Hello World !"', () => {
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  });
});

describe("Sokoban", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/sokoban/sokoban.gb")
  );

  test("Shows the first maze", () => {
    gb.steps(40);
    expect(gb.frameSha).toBe(
      "9092f5818f750dbef06a046314fb0d457229e3c8dabaa67aadca8858399d07e2"
    );
  });

  test("The player moves up if UP is pressed", () => {
    gb.steps(40);
    gb.input(gb.K_UP);
    gb.steps(40);
    expect(gb.frameSha).toBe(
      "fb4f0eb287561d97413ff3a6b7ac0d24404105c227e57b05291b06d25eee3e03"
    );
  });
});

describe("Aces Up", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/aces-up/aces-up.gb")
  );

  test("Shows the title screen", () => {
    gb.steps(40);
    expect(gb.frameSha).toBe(
      "0d75c5bd83e93944fc69dfefbffdd65b43e3818ee954b134117c096bc3806b35"
    );
  });

  test("Starts the game if START is pressed", () => {
    gb.steps(40);
    gb.input(gb.K_START);
    gb.steps(100);
    expect(gb.frameSha).toBe(
      "e2013e01022a8aaf6d08329c2c7ea4121e461f5cea0b5016c6146046ffa5fa2c"
    );
  });
});

describe("Brainfuck", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/brainfuck/brainfuck.gb")
  );

  test('Outputs "Hello World!"', () => {
    gb.steps(75);
    expect(gb.frameSha).toBe(
      "a73773c2a18aa09afa0e2c78737b857fbe57ffc30cca043b66c0883e56d31a0b"
    );
  });
});
