\ Bitcoin miner for Game Boy
\ lf94 (inbox@leefallat.ca) 2024-04-21

0 constant 4TH.GFORTH
1 constant 4TH.GBFORTH

\ change this constant when using different forth systems
4TH.GBFORTH constant 4TH

4TH 4TH.GBFORTH = [if]
  require ibm-font.fs
  require term.fs
[then]

\ printing routines
: hex-byte-to-chars ( u -- c c )
  dup 4 rshift %00001111 and
  dup 9 <= if 48 else 65 10 - then + swap
               %00001111 and
  dup 9 <= if 48 else 65 10 - then +
;

: print-hex ( c-addr u x y -- )
  at-xy
  0 do
    dup I + c@ hex-byte-to-chars emit emit
  loop
  drop
;

\ 32-bit operations
\ separate into its own file, very useful :)

: bits 8 / ;
: bytes 8 * ;

: endian ( a -- a )
  dup           $FF and
  swap 8 rshift $FF and
;

: 4c! ( a b c d addr -- )
  >r
  r@ 3 + c! r@ 2 + c! r@ 1 + c! r@     c!
  r> drop
;

: 4c@ ( addr -- a b c d )
  >r
  r@     c@ r@ 1 + c@ r@ 2 + c@ r@ 3 + c@
  r> drop
;

4TH 4TH.GBFORTH = [if] ram [then]
create a1 1 allot create b1 1 allot
create a2 1 allot create b2 1 allot
create a3 1 allot create b3 1 allot
create a4 1 allot create b4 1 allot

: 32rr ( a b c d n -> a b c d )
  >r a4 c! a3 c! a2 c! a1 c!
  begin r@ 0 > while
    a1 c@ %00000001 and if %10000000 else %00000000 then
    a2 c@ %00000001 and if %10000000 else %00000000 then
    a3 c@ %00000001 and if %10000000 else %00000000 then
    a4 c@ %00000001 and if %10000000 else %00000000 then
    a1 c@ 1 rshift or a1 !
    a4 c@ 1 rshift or a4 !
    a3 c@ 1 rshift or a3 !
    a2 c@ 1 rshift or a2 !
    r> 1 - >r
  repeat
  r> drop a1 c@ a2 c@ a3 c@ a4 c@
;

: 32rs ( a b c d n -> a b c d )
  >r a4 c! a3 c! a2 c! a1 c!
  begin r@ 0 > while
    a1 c@ %00000001 and if %10000000 else %00000000 then
    a2 c@ %00000001 and if %10000000 else %00000000 then
    a3 c@ %00000001 and if %10000000 else %00000000 then
    a4 c@ 1 rshift or a4 !
    a3 c@ 1 rshift or a3 !
    a2 c@ 1 rshift or a2 !
    a1 c@ 1 rshift    a1 !
    r> 1 - >r
  repeat
  r> drop a1 c@ a2 c@ a3 c@ a4 c@
;

: 32xor ( a b c d e f g h -> a b c d )
  b4 c! b3 c! b2 c! b1 c!
  a4 c! a3 c! a2 c! a1 c!

  a1 c@ b1 c@ xor
  a2 c@ b2 c@ xor
  a3 c@ b3 c@ xor
  a4 c@ b4 c@ xor
;

: 32and ( a b c d e f g h -> a b c d )
  b4 c! b3 c! b2 c! b1 c!
  a4 c! a3 c! a2 c! a1 c!

  a1 c@ b1 c@ and
  a2 c@ b2 c@ and
  a3 c@ b3 c@ and
  a4 c@ b4 c@ and
;

: 32not ( a b c d -> a b c d )
  $FF xor a4 c! $FF xor a3 c! $FF xor a2 c! $FF xor a1 c!
  a1 c@ a2 c@ a3 c@ a4 c@
;

: 32+ ( a b c d e f g h -> a b c d )
  b4 c! b3 c! b2 c! b1 c!
  a4 c! a3 c! a2 c! a1 c!

           a4 c@ b4 c@ +   dup $FF and a4 c!
  8 rshift a3 c@ b3 c@ + + dup $FF and a3 c!
  8 rshift a2 c@ b2 c@ + + dup $FF and a2 c!
  8 rshift a1 c@ b1 c@ + +     $FF and a1 c!

  a1 c@ a2 c@ a3 c@ a4 c@
;

: 4dup ( a b c d -> a b c d e f g h )
  a4 c! a3 c! a2 c! a1 c!
  a1 c@ a2 c@ a3 c@ a4 c@
  a1 c@ a2 c@ a3 c@ a4 c@
;

4TH 4TH.GBFORTH = [if] ram [then]
create h* 1 cells allot

: h[].size
  32 bits 8 *
;

: h[] ( i -- addr )
  32 bits * h* @ +
;

: h[].free
  h[].size -1 * allot
;

: h[].init
  here h* h[].size allot !

  $6a $09 $e6 $67 0 h[] 4c!
  $bb $67 $ae $85 1 h[] 4c!
  $3c $6e $f3 $72 2 h[] 4c!
  $a5 $4f $f5 $3a 3 h[] 4c!
  $51 $0e $52 $7f 4 h[] 4c!
  $9b $05 $68 $8c 5 h[] 4c!
  $1f $83 $d9 $ab 6 h[] 4c!
  $5b $e0 $cd $19 7 h[] 4c!
;

4TH 4TH.GBFORTH = [if] rom [then]
create k*
$42 c, $8a c, $2f c, $98 c,
$71 c, $37 c, $44 c, $91 c,
$b5 c, $c0 c, $fb c, $cf c,
$e9 c, $b5 c, $db c, $a5 c,
$39 c, $56 c, $c2 c, $5b c,
$59 c, $f1 c, $11 c, $f1 c,
$92 c, $3f c, $82 c, $a4 c,
$ab c, $1c c, $5e c, $d5 c,
$d8 c, $07 c, $aa c, $98 c,
$12 c, $83 c, $5b c, $01 c,
$24 c, $31 c, $85 c, $be c,
$55 c, $0c c, $7d c, $c3 c,
$72 c, $be c, $5d c, $74 c,
$80 c, $de c, $b1 c, $fe c,
$9b c, $dc c, $06 c, $a7 c,
$c1 c, $9b c, $f1 c, $74 c,
$e4 c, $9b c, $69 c, $c1 c,
$ef c, $be c, $47 c, $86 c,
$0f c, $c1 c, $9d c, $c6 c,
$24 c, $0c c, $a1 c, $cc c,
$2d c, $e9 c, $2c c, $6f c,
$4a c, $74 c, $84 c, $aa c,
$5c c, $b0 c, $a9 c, $dc c,
$76 c, $f9 c, $88 c, $da c,
$98 c, $3e c, $51 c, $52 c,
$a8 c, $31 c, $c6 c, $6d c,
$b0 c, $03 c, $27 c, $c8 c,
$bf c, $59 c, $7f c, $c7 c,
$c6 c, $e0 c, $0b c, $f3 c,
$d5 c, $a7 c, $91 c, $47 c,
$06 c, $ca c, $63 c, $51 c,
$14 c, $29 c, $29 c, $67 c,
$27 c, $b7 c, $0a c, $85 c,
$2e c, $1b c, $21 c, $38 c,
$4d c, $2c c, $6d c, $fc c,
$53 c, $38 c, $0d c, $13 c,
$65 c, $0a c, $73 c, $54 c,
$76 c, $6a c, $0a c, $bb c,
$81 c, $c2 c, $c9 c, $2e c,
$92 c, $72 c, $2c c, $85 c,
$a2 c, $bf c, $e8 c, $a1 c,
$a8 c, $1a c, $66 c, $4b c,
$c2 c, $4b c, $8b c, $70 c,
$c7 c, $6c c, $51 c, $a3 c,
$d1 c, $92 c, $e8 c, $19 c,
$d6 c, $99 c, $06 c, $24 c,
$f4 c, $0e c, $35 c, $85 c,
$10 c, $6a c, $a0 c, $70 c,
$19 c, $a4 c, $c1 c, $16 c,
$1e c, $37 c, $6c c, $08 c,
$27 c, $48 c, $77 c, $4c c,
$34 c, $b0 c, $bc c, $b5 c,
$39 c, $1c c, $0c c, $b3 c,
$4e c, $d8 c, $aa c, $4a c,
$5b c, $9c c, $ca c, $4f c,
$68 c, $2e c, $6f c, $f3 c,
$74 c, $8f c, $82 c, $ee c,
$78 c, $a5 c, $63 c, $6f c,
$84 c, $c8 c, $78 c, $14 c,
$8c c, $c7 c, $02 c, $08 c,
$90 c, $be c, $ff c, $fa c,
$a4 c, $50 c, $6c c, $eb c,
$be c, $f9 c, $a3 c, $f7 c,
$c6 c, $71 c, $78 c, $f2 c,

: k[] 32 bits * k* + ;

\ How many of something you need to pad to the target b.
: pad_to ( a b - c ) swap over mod over - abs swap drop ;

: K_to_pad ( n:bits -- m:bits )
  8 + 64 + 512 pad_to
;

\ From Wikipedia:
\ Pre-processing (Padding):
\ begin with the original message of length L bits
\ append a single '1' bit
\ append K '0' bits, where K is the minimum number >= 0 such that (L + 1 + K + 64) is a multiple of 512
\ append L as a 64-bit big-endian integer, making the total post-processed length a multiple of 512 bits
\ such that the bits in the message are: <original message of length L> 1 <K zeros> <L as 64 bit integer> , (the number of bits will be a multiple of 512)
: new_512_bits_aligned ( addr n -- addr n )
  here >r
  \ Calculate the new pre-processed message size
  ( n )
  dup bytes  dup K_to_pad  +
  1 + 7 + 64 +
  bits here 1 cells allot !           \ data size (multiple of 512 bits)
  here 1 cells + here 1 cells allot ! \ data pointer

  dup >r                     \ message length to append at end
  dup bytes K_to_pad bits >r \ calculate K bits to pad with

  here swap ( addr here n )  dup allot  cmove \ copy original message

  %10000000 here  1 allot  c! \ append %10000000 byte
  here r>  dup allot  0 fill  \ append K 0 bits

  \ adjust to len cell size
  here 6  dup allot  0 fill \ append message length as 64-bit big-endian integer
  here r> bytes 2 allot
  \ avoid endianmess (get it?)
  over over $FF00 and 8 rshift swap     c!
                               swap 1 + c!

  \ Assert our new message length matches what we calculated
  r@ 1 cells + @
  r@ @
  r> drop
;
: s0 ( w-addr -- a b c d )
  15 32 bits * - >r
  r@ 4c@ 7 32rr
  r@ 4c@ 18 32rr
  32xor
  r@ 4c@ 3 32rs
  32xor 
  r> drop
;
: s1 ( w-addr -- a b c d )
  2 32 bits * - >r
  r@ 4c@ 17 32rr
  r@ 4c@ 19 32rr 32xor
  r@ 4c@ 10 32rs 32xor
  r> drop  
;

4TH 4TH.GBFORTH = [if] ram [then]
create w* 1 cells allot

: w[]       32 bits * w* @ + ;
: w[].allot here w* 4 64 * allot ! ;
: w[].free  4 -64 * allot ;

: part1 ( -> )
16 begin dup 64 < while
  >r
  r@ w[] s0
  r@ 16 - w[] 4c@ 32+
  r@ w[] s1 32+
  r@ 7 - w[] 4c@ 32+
  r@ w[] 4c!
  r> 1 +  
repeat drop
;

4TH 4TH.GBFORTH = [if] ram [then]
create a 4 allot
create b 4 allot
create c 4 allot
create d 4 allot
create e 4 allot
create f 4 allot
create g 4 allot
create h 4 allot 

: z1
  e 4c@ 6 32rr
  e 4c@ 11 32rr 32xor
  e 4c@ 25 32rr 32xor
;
: ch
  e 4c@ f 4c@ 32and
  e 4c@ 32not g 4c@ 32and
  32xor
;

4TH 4TH.GBFORTH = [if] ram [then]
create tmp 4 allot

: temp1 ( i -> a b c d )
  >r
  h 4c@ z1 32+
  ch 32+
  4dup tmp 4c!
  r@ k[] 4c@ 32+
  r@ w[] 4c@ 32+
  r> drop
;

: z0 ( -> a b c d )
  a 4c@ 2 32rr
  a 4c@ 13 32rr 32xor
  a 4c@ 22 32rr 32xor
;

: maj ( -> a b c d )
  a 4c@ b 4c@ 32and
  a 4c@ c 4c@ 32and 32xor
  b 4c@ c 4c@ 32and 32xor
;

: temp2 ( -> a b c d )
  z0 maj 32+
;

: part2 ( -> )
  0 h[] a 32 bits cmove
  1 h[] b 32 bits cmove
  2 h[] c 32 bits cmove
  3 h[] d 32 bits cmove
  4 h[] e 32 bits cmove
  5 h[] f 32 bits cmove
  6 h[] g 32 bits cmove
  7 h[] h 32 bits cmove

  0 begin dup 64 < while
    >r
    temp2
    r@ temp1
    4dup d 4c@ 32+
    g h 32 bits cmove
    f g 32 bits cmove
    e f 32 bits cmove
    ( temp1 d 32+ ) e 4c!
    c d 32 bits cmove
    b c 32 bits cmove
    a b 32 bits cmove
    ( temp2 temp1 ) 32+ a 4c!
    r> 1 +
  repeat drop
;

: sha256 ( addr n -> addr n )
  new_512_bits_aligned 

  h[].init
  w[].allot

  0 >r
  begin r@ over < while
    \ copy 16 32-bit words from message[i] to w[0..16]
    over r@ +  w* @  16 32 bits *  cmove
    part1
    part2
    0 h[] 4c@ a 4c@ 32+ 0 h[] 4c!
    1 h[] 4c@ b 4c@ 32+ 1 h[] 4c!
    2 h[] 4c@ c 4c@ 32+ 2 h[] 4c!
    3 h[] 4c@ d 4c@ 32+ 3 h[] 4c!
    4 h[] 4c@ e 4c@ 32+ 4 h[] 4c!
    5 h[] 4c@ f 4c@ 32+ 5 h[] 4c!
    6 h[] 4c@ g 4c@ 32+ 6 h[] 4c!
    7 h[] 4c@ h 4c@ 32+ 7 h[] 4c!
    r> 512 bits + >r
  repeat
  r> drop

  w[].free
  h[].free

  -1 * allot \ free message data
  drop

  \ return data, len
  h* @ 4 8 *
;

4TH 4TH.GBFORTH = [if] ram [then]
create nonce 4 allot

4TH 4TH.GBFORTH = [if] rom [then]
create ffffffff
$ff c, $ff c, $ff c, $ff c,

create bitcoin-block-header-0
$01 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$00 c, $00 c, $00 c, $00 c,
$3b c, $a3 c, $ed c, $fd c,
$7a c, $7b c, $12 c, $b2 c,
$7a c, $c7 c, $2c c, $3e c,
$67 c, $76 c, $8f c, $61 c,
$7f c, $c8 c, $1b c, $c3 c,
$88 c, $8a c, $51 c, $32 c,
$3a c, $9f c, $b8 c, $aa c,
$4b c, $1e c, $5e c, $4a c,
$29 c, $ab c, $5f c, $49 c,
$ff c, $ff c, $00 c, $1d c,
\ $1d c, $ac c, $2b c, $7c c, \ the original successful nonce
$00 c, $00 c, $00 c, $00 c,
here bitcoin-block-header-0 - constant bitcoin-block-header-size

4TH 4TH.GBFORTH = [if] ram [then]
create bitcoin-block-header bitcoin-block-header-size allot

4TH 4TH.GBFORTH = [if]
: init
  install-font
  init-term
;
[then]

4TH 4TH.GFORTH = [if] : init ; [then]

: main
  init

  s" Computing SHA256" 2 1 at-xy type
  s" of" 9 2 at-xy type
  s" Bitcoin block 0" 2 3 at-xy type

  $1d $ac $2b $7c nonce 4c!
  \ it should stop at $1d $ac $2b $7c

  h[].init
  begin
    h* @ 256 bits + 5 - c@ 0 <> >r
    h* @ 256 bits + 4 - c@ 0 <> >r
    h* @ 256 bits + 3 - c@ 0 <> >r
    h* @ 256 bits + 2 - c@ 0 <> >r
    h* @ 256 bits + 1 - c@ 0 <>
    r> or r> or r> or r> or
  while
    bitcoin-block-header-0 bitcoin-block-header bitcoin-block-header-size cmove
    nonce  bitcoin-block-header bitcoin-block-header-size + 4 -  4 cmove
    bitcoin-block-header bitcoin-block-header-size sha256 sha256 drop drop
    0 0 0 1 nonce 4c@ 32+ nonce 4c!
  repeat
  0 h[] 4 2 0 + 5 print-hex
  1 h[] 4 2 8 + 5 print-hex
  2 h[] 4 2 0 + 6 print-hex
  3 h[] 4 2 8 + 6 print-hex
  4 h[] 4 2 0 + 7 print-hex
  5 h[] 4 2 8 + 7 print-hex
  6 h[] 4 2 0 + 8 print-hex
  7 h[] 4 2 8 + 8 print-hex
;

4TH 4TH.GFORTH = [if] main [then]
