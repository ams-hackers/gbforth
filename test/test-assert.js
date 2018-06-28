const fs = require("fs");
const path = require("path")

const gb = require("./gbtest")(__filename);

test("assert", (cb) => {
  gb.steps(200);
  fs.writeFile(
    path.resolve(__dirname, "test-assert.png"),
    gb.frame,
    cb
  )
});
