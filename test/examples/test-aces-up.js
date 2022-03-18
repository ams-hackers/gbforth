const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/aces-up/aces-up.gb")
);

describe("Aces Up", () => {
  test("Shows the title screen", () => {
    gb.run();
    expect(gb.frameSha).toBe(
      "9bef3f63207622052eb9943472880de3bcc9394d832268f2e7b9ef2f5416f18a"
    );
  });

  test("Starts the game if START is pressed", () => {
    gb.run()
    gb.input(gb.K_START);
    gb.run();
    expect(gb.frameSha).toBe(
      "aed9d721f6a388a0c9840c9b4ab17f7cad7bec5bfae6ead0810d085e2bef6921"
    );
  });
});
