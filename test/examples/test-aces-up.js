const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/aces-up/aces-up.gb")
);

describe("Aces Up", () => {
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
