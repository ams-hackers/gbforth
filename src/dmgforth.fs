(
dmgforth.fs ---
)

vocabulary dmgforth
get-current
also dmgforth definitions
constant previous-wid

require ./rom.fs

previous-wid set-current
previous
