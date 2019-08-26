require ./gbhw.fs

: emit
  rSB c!
  \ request serial transfert
  $81 rSC c@ or rSC c! ;
