require strings.fs

create note 100 chars allot

: main
  s" Forth "       note   place
  4                note #+place
  s"  the "        note  +place
  [char] g toupper note c+place
  [char] B toupper note c+place
  note ;
