const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/sokoban/sokoban.gb")
);

describe("Sokoban", () => {
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
