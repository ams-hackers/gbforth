# Forth crash course

If you are new to Forth, it's good to cover some of the basics of the language
before diving into gbforth. This crash course helps you read and follow (on a
very basic level) the code in the examples and documentation.

## Words

Forth is a simple stack-based language: There is essentially no syntax, just
**numbers** and **words**. Numbers are literal values that are pushed on the
parameter **stack**. Words are subroutines that operate on this stack. For
example, consider the following Forth code:

```fs
4 dup *
```

The system starts interpreting this code. First it pushes `4` to the stack. Upon
reading the word `dup`, the value on top of the stack is duplicated. After that,
the word `*` will pop the top 2 values off the stack, multiply them, and push
the result back on the stack.

At the end of this program, the parameter stack will have a single value on it,
which is `16`.

## Defining words

You can also add new words to the Forth dictionary:

```fs
: square dup * ;
```

Once the word `:` is read, the system parses the next word and uses it as the
name for your new definition.

It then switches to **compiler mode**, which means that it stops interpreting
code, but instead add words (and numbers) to the body of the new definition.

After some time the word `;` is read. This words ends the definition and
switches the system back to **interpreter mode**. With this new word defined, we
can rewrite our earlier example:

```fs
: square dup * ;

4 square
```

This code will have the same effect as before: The parameter stack contains a
single `16`.

Take note of the concatenative style of Forth: Words are always evaluated from
left to right, and _concatenation is composition_. Any series of words can be
moved into a new definition without having to shuffle words around. In practice,
this means you can safely replace all occurrences of `dup *` with `square`, so
simplifying your code is very straightforward!

## Comments

There are 2 styles of comments: In-line comments that are delimited between `(`
and `)`, and comments that start with `\` and continue till the end of the line:

```fs
: square ( this is a comment ) dup * ;

\ and this too
4 square
```

Note that this is no special syntax: Both `(` and `\` are simply **words** that
parse and discard all characters until a `)` or line end is reached. Make sure
to not forget to follow these words with a space before writing your comment.

It's common to use in-line comments to document the
[stack effect](./stack-effects.md) of a word:

```fs
: square ( n -- n ) dup * ;
```

## Further reading

If you want to learn about Forth on a deeper level,
[Starting FORTH](https://www.forth.com/starting-forth) is a great read!
