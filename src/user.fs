( User Vocabulary )

: export
  parse-name 
  2dup find-name name>int >r
  nextname r> alias ;

: [user-definitions]
  get-current
  also dmgforth-user definitions ;
  
: [end-user-definitions]
  previous set-current ;

[user-definitions]
also dmgforth

: [host] also forth ;
: [endhost] previous ;

: c, rom, ;

export ( 
export ==>
export \
export also
export constant
export gb-assembler
export include
export previous
export require

previous
[end-user-definitions]
