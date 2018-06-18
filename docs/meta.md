# Meta Programming
Depending on where code is written, words can be executed on either the _host_ or _target_ system. Similarly, when reading and writing data, you may reference either the _host_ or _target_ data.

## Top-level code
By default, all words are _executed on the **host**_ system, and operate on _data in the **target**_:

```forth
CREATE foo 2 cells allot
```

This code would create the word `foo` at compile-time, and reserve memory in the target ROM.

## Colon definitions
Words inside colon definitions are _executed on the **target**_ system, and operate on _data in the **target**_ as well:

```forth
: bar
  %11100100 $FF47 ! ;
```

The words in the definition of `bar` would execute at run-time, storing a value to an address on the target.

## Host definitions
Sometimes you'll want to write code that is _executed on the **host**_ system, and operates on _data in the **host**_ as well:

```forth
[host]
: baz
  depth 0<> abort" Stack is not empty"
[endhost]
```

The word `baz` is defined on the host only, and can only be called from a top-level context. When accessing data, the definition is referencing the host memory (and stack).

## Summary

| Context              | Execution             | Data                 |
|----------------------|-----------------------|----------------------|
| Top-level            | Host *(compile-time)* | Target *(ROM)*       |
| Colon definition `:` | Target *(run-time)*   | Target *(ROM / RAM)* |
| `[host]`             | Host *(compile-time)* | Host                 |
