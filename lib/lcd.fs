require gbhw.fs

code disable-lcd
  [rLCDC] A ld,
  rlca,

  #NC ret,

  here<
  [rLY] A ld,
  #145 # A cp,
  <there #NZ jr,
  [rLCDC] A ld,
  A #7 # res,
  A [rLCDC] ld,

  ret,
end-code

: enable-lcd
  [ LCDCF_ON
    LCDCF_BG8000 or
    LCDCF_BG9800 or
    LCDCF_BGON or
    LCDCF_OBJ16 or
    LCDCF_OBJOFF or ]L
  rLCDC c! ;
