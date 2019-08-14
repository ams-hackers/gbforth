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
    #145 # A cp,
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
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BG9800 or
    LCDCF_BGON or
    LCDCF_OBJ16 or
    LCDCF_OBJOFF or ]L
  rLCDC c! ;

code wait-lcd
lcd_WaitVRAM
end-code
