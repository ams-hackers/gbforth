require gbhw.fs

[asm]
:m vblank-interrupt:
  ' rom here swap
  $0040 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m lcd-interrupt:
  ' rom here swap
  $0048 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m timer-interrupt:
  ' rom here swap
  $0050 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m serial-interrupt:
  ' rom here swap
  $0058 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m p10-interrupt:
  ' rom here swap
  $0060 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m p11-interrupt:
  ' rom here swap
  $0068 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m p12-interrupt:
  ' rom here swap
  $0070 ==> ( xt ) # call, reti,
  ( here ) ==> ;

:m p13-interrupt:
  ' rom here swap
  $0078 ==> ( xt ) # call, reti,
  ( here ) ==> ;
[endasm]

: ief! ( c -- )
  0 rIF c!
  rIE c@ or rIE c! ;

: enable-vblank-interrupts IEF_VBLANK ief! ; \ $0040
: enable-lcd-interrupts    IEF_LCDC   ief! ; \ $0048
: enable-timer-interrupts  IEF_TIMER  ief! ; \ $0050
: enable-serial-interrupts IEF_SERIAL ief! ; \ $0058
: enable-key-interrupts    IEF_HILO   ief! ; \ $0060 $0068 $0070 $0078
