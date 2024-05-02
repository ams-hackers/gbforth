\ Bitcoin miner for Game Boy
\ lf94 (inbox@leefallat.ca) 2024-04-21

require ibm-font.fs
require term.fs

: 16!
  over          $FF and over 1 + c! 
  over 8 rshift $FF and over     c!
  drop drop
;

: bits 8 / ;
: bytes 8 * ;

: 2cc! ( n1 n2 addr -- )
  >r
  dup      r@ 3 + c!
  8 rshift r@ 2 + c!
  dup      r@ 1 + c!
  8 rshift r@ c!
  r>
  drop
;
: 4c! ( b1 b2 b3 b4 addr -- )
  >r
  r@ 3 + c!
  r@ 2 + c!
  r@ 1 + c!
  r@     c!
  r>
  drop
;
: 32@ ( addr -- n1 n2 )
  >r
  r@     c@ 8 lshift
  r@ 1 + c@ or
  r@ 2 + c@ 8 lshift
  r@ 3 + c@ or
  r>
  drop
;

create 16a 1 cells allot
create 16b 1 cells allot
create 16c 1 cells allot
create 16d 1 cells allot

: 32rr ( a b n -- )
  >r
  16b !
  16a !
  begin r@ 0 > while
    16a @ %0000000000000001 and if %1000000000000000 else %0000000000000000 then
    16b @ %0000000000000001 and if %1000000000000000 else %0000000000000000 then
    16a @ 1 rshift or 16a !
    16b @ 1 rshift or 16b !
    r> 1 - >r
  repeat
  r> drop
  16a @
  16b @
;

: 32rs ( a b n -- )
  >r
  16b !
  16a !
  begin r@ 0 > while
    16a @ %0000000000000001 and if %1000000000000000 else %0000000000000000 then
    16b @ 1 rshift or 16b !
    16a @ 1 rshift 16a !
    r> 1 - >r
  repeat
  r> drop
  16a @ 16b @
;

: 32xor ( a b c d -- e f )
  16d !
  16c !
  16b !
  16a !
  16a @
  16c @
  xor
  16b @
  16d @
  xor
;

: 32and ( a b c d -- e f )
  16d !
  16c !
  16b !
  16a !
  16a @
  16c @
  and
  16b @
  16d @
  and
;

: 32not ( a b -- c d )
  $FFFF xor swap
  $FFFF xor swap
;

: 16+>? ( a b -- n )
  65535 swap - > abs
;

: 16+ ( a b -- n )
  + 65536 mod
;

\ doesn't need to be initialized, will be set first
create carry 1 cells allot

: 32+ ( a b c d -- e f )
  >r 16c ! r>
  over over 16+>? carry !
  16+ >r
  16c @
  carry @ ( a c carry )
  over over 16+>? carry !
  16+ ( a k )
  over over 16+>? carry @ or carry !
  16+ ( f )
  r>
;

: c!+
  over c!
  1 +
;

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

rom
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
  here 6  dup allot  0 fill   \ append message length as 64-bit big-endian integer
  r> bytes here  2 allot  16! \ the actual length (8-bit)

  \ Assert our new message length matches what we calculated
  r@ 1 cells + @
  r@ @
  r> drop
;
: s0 ( w-addr -- a b c d )
  15 32 bits * - >r
  r@ 32@ 7 32rr
  r@ 32@ 18 32rr
  32xor
  r@ 32@ 3 32rs
  32xor 
  r> drop
;
: s1 ( w-addr -- a b c d )
  2 32 bits * - >r
  r@ 32@ 17 32rr
  r@ 32@ 19 32rr 32xor
  r@ 32@ 10 32rs 32xor
  r> drop  
;

ram
create w* 1 cells allot

: w[]
  32 bits * w* @ +
;

: w[].init
  here w* 4 64 * allot !
;

: w[].free
  4 -64 * allot
;

: part1 ( -- )
16 begin dup 64 < while
  >r
  r@ w[] s0
  r@ 16 - w[] 32@ 32+
  r@ w[] s1 32+
  r@ 7 - w[] 32@ 32+
  r@ w[] 2cc!
  r> 1 +  
repeat drop
;

ram
create a 4 allot
create b 4 allot
create c 4 allot
create d 4 allot
create e 4 allot
create f 4 allot
create g 4 allot
create h 4 allot 

: z1
  e 32@ 6 32rr
  e 32@ 11 32rr 32xor
  e 32@ 25 32rr 32xor
;
: ch
  e 32@ f 32@ 32and
  e 32@ 32not g 32@ 32and
  32xor
;
: temp1 ( i -- a b c d )
  >r
  h 32@ z1 32+
  ch 32+
  r@ k[] 32@ 32+
  r@ w[] 32@ 32+
  r> drop
;

: z0
  a 32@ 2 32rr
  a 32@ 13 32rr 32xor
  a 32@ 22 32rr 32xor
;

: maj
  a 32@ b 32@ 32and
  a 32@ c 32@ 32and 32xor 
  b 32@ c 32@ 32and 32xor
;

: temp2 ( -- a b c d )
  z0 maj 32+
;

: part2
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
    2dup d 32@ 32+
    g h 32 bits cmove
    f g 32 bits cmove
    e f 32 bits cmove
    ( temp1 d 32+ ) e 2cc!
    c d 32 bits cmove
    b c 32 bits cmove
    a b 32 bits cmove
    ( temp2 temp1 ) 32+ a 2cc!
    r> 1 +
  repeat drop
;

: sha256 ( addr n -- 256 bits on stack )
  new_512_bits_aligned 

  h[].init
  w[].init

  0 >r
  begin r@ over < while
    \ copy 16 32-bit words from message[i] to w[0..16]
    over r@ +  w* @  16 32 bits *  cmove
    part1
    part2
    0 h[] 32@ a 32@ 32+ 0 h[] 2cc!
    1 h[] 32@ b 32@ 32+ 1 h[] 2cc!
    2 h[] 32@ c 32@ 32+ 2 h[] 2cc!
    3 h[] 32@ d 32@ 32+ 3 h[] 2cc!
    4 h[] 32@ e 32@ 32+ 4 h[] 2cc!
    5 h[] 32@ f 32@ 32+ 5 h[] 2cc!
    6 h[] 32@ g 32@ 32+ 6 h[] 2cc!
    7 h[] 32@ h 32@ 32+ 7 h[] 2cc!
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

: <>32@ ( a b - bool )
  over     c@ over     c@ <> >r
  over 1 + c@ over 1 + c@ <> >r
  over 2 + c@ over 2 + c@ <> >r
       3 + c@ swap 3 + c@ <>
  r> and r> and r> and
;

ram
create nonce 4 allot

rom
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
\ $1d c, $ac c, $2b c, $7c c,
$00 c, $00 c, $00 c, $00 c,
here bitcoin-block-header-0 - constant bitcoin-block-header-size

ram
create bitcoin-block-header bitcoin-block-header-size allot

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

: main
  install-font
  init-term

  s" Computing SHA256" 2 7 at-xy type
  s" of" 9 8 at-xy type
  s" Bitcoin block 0" 2 9 at-xy type

  $1d $ac $2b $79 nonce 4c!
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
    0 1 nonce 32@ 32+ nonce 2cc!

    h* @ 256 bits 0 11 print-hex
  repeat
;
