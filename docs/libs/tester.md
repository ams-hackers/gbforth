# tester.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/tester.fs)

The `tester.fs` library helps you write test assertions.

## Usage

Write test cases between the words `T{` and `T}`, and use `->` to delimit the
program from the expected stack values.

An example test suite might look like this:

```forth
require tester.fs

: main
  T{ 1 2 3 rot     -> 2 3 1   }T
  T{ 4 dup *       -> 16      }T
  T{ 5 6 tuck over -> 6 5 6 5 }T ;
```

## Word Index

##### `T{` _( -- )_

Marks the start of a test case. Mostly syntactic sugar, but also tracks the
current stack depth to preserve values not part of the test.

##### `->` _( ... -- )_

Delimits the code to be tested from the expected stack. Essentially stores the
full stack (up to 32 values) so it can be compared against the expected values
later.

##### `}T` _( ... -- )_

Marks the end of the expected test values (and the test case). This will compare
the current stack to the stored stack values (and reset the stack).

##### `error` _( c-addr u -- )_

A deferred word that receives an error message if a test case failed. By
default, this will immediately stop the execution of the program. You can
override this behaviour by pointing `error-xt` to your own routine:

```forth
\ using an anonymous word
:noname type cr ; error-xt !

\ using an existing word
' report-error error-xt !
```
