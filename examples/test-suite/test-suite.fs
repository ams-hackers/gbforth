\ Based on the ANS test suite by John Hayes,
\ modified to work properly with gbforth.

title: TESTSUITE
gamecode: TEST

require ibm-font.fs
require term.fs
require tester.fs

VARIABLE #total
VARIABLE #fails
: }T 1 #total +! }T ;
:noname 1 #fails +! ; error-xt !

: BITSSET? IF 0 0 ELSE 0 THEN ;
: GR1 >R R> ;
: GR2 >R R@ R> DROP ;

: run-suite

\ ------------------------------------------------------------------------
\ BASIC ASSUMPTIONS

T{ -> }T
T{  0 BITSSET? -> 0 }T		( ZERO IS ALL BITS CLEAR )
T{  1 BITSSET? -> 0 0 }T	( OTHER NUMBER HAVE AT LEAST ONE BIT )
T{ -1 BITSSET? -> 0 0 }T

\ ------------------------------------------------------------------------
\ NUMERIC NOTATION

T{ #1289       -> 1289        }T
T{ #12346789  -> 12346789. }T
T{ #-1289      -> -1289       }T
T{ #-12346789. -> -12346789.  }T
T{ $12eF       -> 4847        }T
T{ $12aBcDeF.  -> 313249263.  }T
T{ $-12eF      -> -4847       }T
T{ $-12AbCdEf. -> -313249263. }T
T{ %10010110   -> 150         }T
T{ %10010110.  -> 150.        }T
T{ %-10010110  -> -150        }T
T{ %-10010110. -> -150.       }T
T{ 'z'         -> 122         }T

\ ------------------------------------------------------------------------
\ TESTING BOOLEANS: INVERT AND OR XOR

T{ 0 0 AND -> 0 }T
T{ 0 1 AND -> 0 }T
T{ 1 0 AND -> 0 }T
T{ 1 1 AND -> 1 }T

T{ 0 INVERT 1 AND -> 1 }T
T{ 1 INVERT 1 AND -> 0 }T

[ 0	       CONSTANT 0S ]
[ 0 INVERT CONSTANT 1S ]

T{ 0S INVERT -> 1S }T
T{ 1S INVERT -> 0S }T

T{ 0S 0S AND -> 0S }T
T{ 0S 1S AND -> 0S }T
T{ 1S 0S AND -> 0S }T
T{ 1S 1S AND -> 1S }T

T{ 0S 0S OR -> 0S }T
T{ 0S 1S OR -> 1S }T
T{ 1S 0S OR -> 1S }T
T{ 1S 1S OR -> 1S }T

T{ 0S 0S XOR -> 0S }T
T{ 0S 1S XOR -> 1S }T
T{ 1S 0S XOR -> 1S }T
T{ 1S 1S XOR -> 0S }T

\ ------------------------------------------------------------------------
\ TESTING BOOLEANS

[ 0S CONSTANT <FALSE> ]
[ 1S CONSTANT <TRUE> ]

T{ 0 0= -> <TRUE> }T
T{ 1 0= -> <FALSE> }T
T{ 2 0= -> <FALSE> }T
T{ -1 0= -> <FALSE> }T

T{ 0 0 = -> <TRUE> }T
T{ 1 1 = -> <TRUE> }T
T{ -1 -1 = -> <TRUE> }T
T{ 1 0 = -> <FALSE> }T
T{ -1 0 = -> <FALSE> }T
T{ 0 1 = -> <FALSE> }T
T{ 0 -1 = -> <FALSE> }T

T{ 0 0< -> <FALSE> }T
T{ -1 0< -> <TRUE> }T
T{ 1 0< -> <FALSE> }T

T{ 0 1 < -> <TRUE> }T
T{ 1 2 < -> <TRUE> }T
T{ -1 0 < -> <TRUE> }T
T{ -1 1 < -> <TRUE> }T
T{ 0 0 < -> <FALSE> }T
T{ 1 1 < -> <FALSE> }T
T{ 1 0 < -> <FALSE> }T
T{ 2 1 < -> <FALSE> }T
T{ 0 -1 < -> <FALSE> }T
T{ 1 -1 < -> <FALSE> }T

T{ 0 1 > -> <FALSE> }T
T{ 1 2 > -> <FALSE> }T
T{ -1 0 > -> <FALSE> }T
T{ -1 1 > -> <FALSE> }T
T{ 0 0 > -> <FALSE> }T
T{ 1 1 > -> <FALSE> }T
T{ 1 0 > -> <TRUE> }T
T{ 2 1 > -> <TRUE> }T
T{ 0 -1 > -> <TRUE> }T
T{ 1 -1 > -> <TRUE> }T

T{ 0 1 MIN -> 0 }T
T{ 1 2 MIN -> 1 }T
T{ -1 0 MIN -> -1 }T
T{ -1 1 MIN -> -1 }T
T{ 0 0 MIN -> 0 }T
T{ 1 1 MIN -> 1 }T
T{ 1 0 MIN -> 0 }T
T{ 2 1 MIN -> 1 }T
T{ 0 -1 MIN -> -1 }T
T{ 1 -1 MIN -> -1 }T

T{ 0 1 MAX -> 1 }T
T{ 1 2 MAX -> 2 }T
T{ -1 0 MAX -> 0 }T
T{ -1 1 MAX -> 1 }T
T{ 0 0 MAX -> 0 }T
T{ 1 1 MAX -> 1 }T
T{ 1 0 MAX -> 1 }T
T{ 2 1 MAX -> 2 }T
T{ 0 -1 MAX -> 0 }T
T{ 1 -1 MAX -> 1 }T

\ ------------------------------------------------------------------------
\ TESTING INTEGERS

[
%1111111111111111 CONSTANT MAX-UINT
%0111111111111111 CONSTANT MAX-INT
%1000000000000000 CONSTANT MIN-INT
]

T{ MIN-INT 0= -> <FALSE> }T
T{ MAX-INT 0= -> <FALSE> }T

T{ MIN-INT 0< -> <TRUE> }T
T{ MAX-INT 0< -> <FALSE> }T

T{ MIN-INT 0 < -> <TRUE> }T
T{ 0 MAX-INT < -> <TRUE> }T
T{ MAX-INT 0 < -> <FALSE> }T

T{ 0 MAX-INT > -> <FALSE> }T
T{ 0 MIN-INT > -> <TRUE> }T
T{ MAX-INT 0 > -> <TRUE> }T

T{ 0 MAX-UINT U< -> <TRUE> }T
T{ MAX-UINT 0 U< -> <FALSE> }T

T{ MIN-INT 0 MIN -> MIN-INT }T
T{ 0 MAX-INT MIN -> 0 }T
T{ MAX-INT 0 MIN -> 0 }T

T{ 0 MAX-INT MAX -> MAX-INT }T
T{ 0 MIN-INT MAX -> 0 }T
T{ MAX-INT 0 MAX -> MAX-INT }T

\ ------------------------------------------------------------------------
\ TESTING STACK OPS: 2DROP 2DUP 2OVER 2SWAP ?DUP DEPTH DROP DUP OVER ROT SWAP

T{ 1 2 2DROP -> }T
T{ 1 2 2DUP -> 1 2 1 2 }T
T{ 1 2 3 4 2OVER -> 1 2 3 4 1 2 }T
T{ 1 2 3 4 2SWAP -> 3 4 1 2 }T
T{ 0 ?DUP -> 0 }T
T{ 1 ?DUP -> 1 1 }T
T{ -1 ?DUP -> -1 -1 }T
T{ DEPTH -> 0 }T
T{ 0 DEPTH -> 0 1 }T
T{ 0 1 DEPTH -> 0 1 2 }T
T{ 0 DROP -> }T
T{ 1 2 DROP -> 1 }T
T{ 1 DUP -> 1 1 }T
T{ 1 2 OVER -> 1 2 1 }T
T{ 1 2 3 ROT -> 2 3 1 }T
T{ 1 2 SWAP -> 2 1 }T

\ ------------------------------------------------------------------------
\ TESTING >R R> R@

T{ 123 GR1 -> 123 }T
T{ 123 GR2 -> 123 }T
T{ 1S GR1 -> 1S }T   ( RETURN STACK HOLDS CELLS )

;

: main
  install-font
  init-term

  0 #total !
  0 #fails !

  ." Running tests..." cr
  run-suite cr

  #total @ #fails @ - 4 .r space ." passed" cr
  #fails @ 4 .r space ." failed" cr cr

  #fails @ 0= IF ." SUCCESS!" THEN ;