require gbhw.fs

[asm]
:m lcd-wait-vram,
  begin,
    [rSTAT] a ld,
    STATF_BUSY # A and,
  #Z until, ;

:m lcd-wait-vblank,
  begin,
    [rLY] A ld,
    #144 # A cp,
  #Z until, ;
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
