const path = require("path");
const gb = require("../gbtest")(
  path.resolve(__dirname, "../../examples/hello-world/hello.gb")
);

describe("hello-world", () => {
  test('Shows "Hello World !"', () => {
    gb.run();
    expect(gb.frameSha).toBe(
      "88801720e7ad041a76a1fd21e2280b113b4cb80a9b2131b4b948fe680b577284"
    );
  });
});
