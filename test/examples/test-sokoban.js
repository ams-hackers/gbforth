const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/sokoban/sokoban.gb")
);

describe("Sokoban", () => {
  test("Shows the first maze", () => {
    gb.run();
    expect(gb.frameSha).toBe(
      "ccc593ed7ed2d35af08774336d0d67ac3a65f20ad46f104927fb16dad9c75b13"
    );
  });

  test("The player moves up if UP is pressed", () => {
    gb.run();
    gb.input(gb.K_UP);
    gb.run();
    expect(gb.frameSha).toBe(
      "e6f27c478752534476b245810b7ce0c41daec0b3fb41ce3e0eb5ed31e854565d"
    );
  });
});
