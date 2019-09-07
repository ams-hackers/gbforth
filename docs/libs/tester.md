# tester.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/tester.fs)

The `tester.fs` library helps you write test assertions.

## Usage

Initialise the lib by calling `init-tester` first. This resets the test error
counter (the variable `#errors`) to 0.

Write test cases between the words `T{` and `T}`, and use `->` to delimit the
program from the expected stack values.

An example test suite might look like this:

```forth

require tester.fs

: run-tests
  T{ 1 2 3 rot     -> 2 3 1   }T
  T{ 4 dup *       -> 16      }T
  T{ 5 6 tuck over -> 6 5 6 5 }T ;

: main
  init-tester
  run-tests
  #errors ? ." failures" ;
```

## Word Index

##### `T{` _( ... -- )_

Marks the start of a test case. Mostly syntactic sugar, but also clears the full
stack first.

##### `->` _( ... -- )_

Delimits the code to be tested from the expected stack. Essentially stores the
full stack (up to 20 values) so it can be compared against the expected values
later.

##### `}T` _( ... -- )_

Marks the end of the expected test values (and the test case). This will compare
the current stack to the stored stack values (and clear the stack). Increments
the `#errors` variable by 1 in case of mismatch.
