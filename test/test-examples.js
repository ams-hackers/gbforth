const path = require("path");

describe("sokoban", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/sokoban/sokoban.gb")
  );

  test("shows the first maze", () => {
    gb.steps(40);
    expect(gb.frameSha).toBe(
      "0dd7aa501f46280525e35dbd2e25a19c07fe8de0c811cd82f5555dfebe3ce837"
    );
  });

  test("The player moves up if UP is pressed", () => {
    gb.steps(40);
    gb.gameboy._joypad.keyDown(38);
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "c5e08dfb1bdb066ff678a97c287136fd5122675397aa9ad496f8e57fa851c613"
    );
  });
});

describe("hello-world", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/hello-world/hello.gb")
  );

  test("shows hello world screen", () => {
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  });
});

describe("hello-world-asm", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/hello-world-asm/hello.gb")
  );

  test("shows hello world screen", () => {
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  });
});
