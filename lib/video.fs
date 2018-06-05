require gbhw.fs

[host]
[asm]

: lcd_WaitVRAM
  here<
    [rSTAT] a ld,
    STATF_BUSY # A and,
  <there #nz jr, ;

[endasm]
[endhost]
