# Memory Spaces (ROM/RAM)

Words that act on memory in the target can either reference the **RAM**
(read/write) or the **ROM** (read-only). The available data space is dependent
on whether it is being accessed during compile-time or run-time.

## Compile-time
During **compile-time**, you'll reference **RAM** by default. You're able to
select the affected data space using `ROM` and `RAM` for some words.

This is the only time where the ROM is writable (since gbforth is generating the
program), and generally speaking, this is where you want to store most of your
data.

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
| `variable` | ROM/RAM |

As you can see, you are not able to initialise, read from or write to the RAM at
compile-time. Keep in mind that you can only _reserve_ space in the RAM (and
name the space with words such as `CREATE` and `VARAIBLE`), but gbforth will
**not** initialise (or zero) this memory for you.

If you need to initialise the RAM, you'll need to do this at run-time (meaning
you might have to store some data in the ROM, and then copy it over on startup).

## Run-time
During **run-time**, you are unable to modify the ROM, so you always reference
the **RAM** when trying to write or allocate memory. As such, the words `ROM`
and `RAM` are not available here.

The ROM is still accessible for reading, but since `@` and `c@` read from a
specific memory _address_, the memory space is implicit.

Additionally, words like `CREATE` and `VARIABLE` are not available due to the
target not having an input steam to parse the name from.

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
