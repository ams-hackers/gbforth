require gbhw.fs

[asm]
:m lcd_WaitVRAM
  here<
    [rSTAT] a ld,
    STATF_BUSY # A and,
  <there #nz jr, ;

:m lcd-wait-vblank,
  here<
    [rLY] A ld,
    #144 # A cp,
  <there #NZ jr, ;
[endasm]

code disable-lcd
  [rLCDC] A ld,
  rlca,
  #NC ret,

  lcd-wait-vblank,

  rLCDC # HL ld,
  [HL] #7 # res,
end-code

: enable-lcd
  rLCDC c@
  LCDCF_ON or
  rLCDC c! ;

code lcd-wait-vblank
lcd-wait-vblank,
end-code
