const path = require("path");

describe("sokoban", () => {
  const gb = require("./gbtest")(
    path.resolve(__dirname, "../examples/sokoban/sokoban.gb")
  );

  test("shows the first maze", () => {
    gb.steps(40);
    expect(gb.frameSha).toBe(
      "1cfbf989e4053d75336160777db5dd56c385c1ccd7a0e47f5dc9a50e94b133ce"
    );
  });

  test("The player moves up if UP is pressed", () => {
    gb.steps(40);
    gb.gameboy._joypad.keyDown(38);
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "5952414b1881adb2ad83f5ac410d2092b16b8508d9866c7b45e99be4afe722e5"
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
