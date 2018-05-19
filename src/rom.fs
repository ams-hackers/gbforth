require ./utils.fs

32 kB constant rom-size
create rom-base rom-size allot
variable rom-offset-variable

: rom-offset rom-offset-variable @ ;
: rom-offset! rom-offset-variable ! ;
: rom-offset+! rom-offset-variable +! ;

: rom rom-base rom-size ;

( Initialize the room to zeros )
rom erase


: offset>addr
  rom-base + ;

: rom@ ( offset -- val )
  offset>addr c@ ;

: rom! ( val offset -- )
  offset>addr c! ;

: rom,
  rom-offset rom!
  $1 rom-offset+! ;

: rom-move ( addr u -- )
  dup >r
  rom-offset offset>addr swap move
  r> rom-offset+! ;

: rom"
  [char] " parse rom-move ;

: ==> ( n -- )
  rom-offset! ;

0 Value rom-fd
: dump-rom ( c-addr u -- )
  w/o bin create-file throw TO rom-fd
  rom rom-fd write-file throw
  rom-fd close-file throw ;
