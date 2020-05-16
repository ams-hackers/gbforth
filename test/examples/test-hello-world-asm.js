const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/hello-world-asm/hello.gb")
);

describe("hello-world-asm", () => {
  test('Shows "Hello World !"', () => {
    gb.steps(10);
    expect(gb.frameSha).toBe(
      "1dded7c5cbaaa4b94377fc76574deffb0869ee65e9b72dfafae0604304fbe365"
    );
  });
});
