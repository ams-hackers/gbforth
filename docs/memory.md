# Memory Spaces (ROM/RAM)

Words that act on memory in the target can either reference the **RAM** (read/write)
or the **ROM** (read-only). The available data space is dependent on whether it is
being accessed during compile-time or run-time.

## Compile-time
During **compile-time**, you'll reference **ROM** by default. This is the only time
where the ROM is writable (since gbforth is generating the program), and generally
speaking, this is where you want to store most of your data.

One exception to this is `VARIABLE`, which will automatically reference the **RAM**.

For other words, you're able to freely select the affected data space using the
words `ROM` and `RAM`.

| Word | Memory |
| ---- | ------ |
| `@` | ROM |
| `c@` | ROM |
| `!` | ROM |
| `c!` | ROM |
| `,` | ROM |
| `c,` | ROM |
| `here` | ROM/RAM |
| `unused` | ROM/RAM |
| `allot` | ROM/RAM |
| `align` | ROM/RAM |
| `aligned` | ROM/RAM |
| `create` | ROM/RAM |
| `variable` | RAM |

As you can see, you are not able to initialise the RAM at compile-time. Keep in
mind that this also applies to `VARIABLE`: This word only reserves a cell in the
RAM, but will **not** initialise or zero this memory for you.

If you need to initialise the RAM, you'll need to do this at run-time.

## Run-time
During **run-time**, you always reference the **RAM**. As such, the words `ROM` and `RAM` are not available here.

Additionally, words like `CREATE` and `VARIABLE` are not available
due to the target not having an input steam to parse the name from.

| Word | Memory |
| ---- | ------ |
| `@` | ROM/RAM |
| `c@` | ROM/RAM |
| `!` | RAM |
| `c!` | RAM |
| `,` | RAM |
| `c,` | RAM |
| `here` | RAM |
| `unused` | RAM |
| `allot` | RAM |
| `align` | RAM |
| `aligned` | RAM |
| `create` |  unavailable |
| `variable` | unavailable |
