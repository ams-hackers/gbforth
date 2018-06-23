# Run-time limitations

It is important to note that gbforth does not provide a run-time Forth system on
the target. While most words will be compiled and work on the target as you would
expect, this imposes some limitations compared to other Forth systems.

This page lists all words that are either unsupported completely, or behave
differently in order to emulate the expected behaviour as close as possible.

## Unsupported

| Word | Reason |
| ---- | ------ |
| `constant` | No input stream available |
| `create` | No input stream available |
| `parse` | No input stream available |
| `postpone` | No input stream available |
| `variable` | No input stream available |

## Partial support

| Word | Alternative behaviour | Reason |
| ---- | --------------------- | ------ |
| `bye` | Terminate execution of the program and _stop_ the CPU | No OS available |
