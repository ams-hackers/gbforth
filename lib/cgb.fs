require ./cartridge.fs

:m enable-cgb CGB_COMPATIBLE $0143 c! ;
: detect-cgb ( -- f ) DP0 c@ $11 = ;
