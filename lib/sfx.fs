require ./gbhw.fs

: beep
  $00 rNR10 c!
  $80 rNR11 c!
  $80 rNR12 c!
  $1C rNR13 c!
  $C7 rNR14 c! ;

: boop
  $00 rNR10 c!
  $80 rNR11 c!
  $80 rNR12 c!
  $EA rNR13 c!
  $C6 rNR14 c! ;

: blip
  $15 rNR10 c!
  $96 rNR11 c!
  $83 rNR12 c!
  $BB rNR13 c!
  $85 rNR14 c! ;

: pow
  $1D rNR10 c!
  $96 rNR11 c!
  $83 rNR12 c!
  $BB rNR13 c!
  $85 rNR14 c! ;

: thud
  $15 rNR10 c!
  $96 rNR11 c!
  $83 rNR12 c!
  $F6 rNR13 c!
  $81 rNR14 c! ;

: buzz
  $00 rNR10 c!
  $00 rNR11 c!
  $84 rNR12 c!
  $00 rNR13 c!
  $80 rNR14 c! ;

: whoosh
  $4F rNR10 c!
  $96 rNR11 c!
  $87 rNR12 c!
  $BB rNR13 c!
  $85 rNR14 c! ;

: init-sfx
  $77 rNR50 c!
  $FF rNR51 c!
  $80 rNR52 c! ;
