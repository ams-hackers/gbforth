#44   constant C3  \ MIDI  36  -   65.406 Hz
#156  constant C#3 \ MIDI  37  -   69.295 Hz
#262  constant D3  \ MIDI  38  -   73.416 Hz
#363  constant D#3 \ MIDI  39  -   77.781 Hz
#457  constant E3  \ MIDI  40  -   82.406 Hz
#547  constant F3  \ MIDI  41  -   87.307 Hz
#631  constant F#3 \ MIDI  42  -   92.499 Hz
#710  constant G3  \ MIDI  43  -   97.998 Hz
#786  constant G#3 \ MIDI  44  -  103.82  Hz
#854  constant A3  \ MIDI  45  -  110.00  Hz
#923  constant A#3 \ MIDI  46  -  116.54  Hz
#986  constant B3  \ MIDI  47  -  123.47  Hz
#1046 constant C4  \ MIDI  48  -  130.81  Hz
#1102 constant C#4 \ MIDI  49  -  138.59  Hz
#1155 constant D4  \ MIDI  50  -  146.83  Hz
#1205 constant D#4 \ MIDI  51  -  155.56  Hz
#1253 constant E4  \ MIDI  52  -  164.81  Hz
#1297 constant F4  \ MIDI  53  -  174.61  Hz
#1339 constant F#4 \ MIDI  54  -  184.99  Hz
#1379 constant G4  \ MIDI  55  -  195.99  Hz
#1417 constant G#4 \ MIDI  56  -  207.65  Hz
#1452 constant A4  \ MIDI  57  -  220.00  Hz
#1486 constant A#4 \ MIDI  58  -  233.08  Hz
#1517 constant B4  \ MIDI  59  -  246.94  Hz
#1546 constant C5  \ MIDI  60  -  261.63  Hz
#1575 constant C#5 \ MIDI  61  -  277.18  Hz
#1602 constant D5  \ MIDI  62  -  293.66  Hz
#1627 constant D#5 \ MIDI  63  -  311.13  Hz
#1650 constant E5  \ MIDI  64  -  329.63  Hz
#1673 constant F5  \ MIDI  65  -  349.23  Hz
#1694 constant F#5 \ MIDI  66  -  369.99  Hz
#1714 constant G5  \ MIDI  67  -  391.99  Hz
#1732 constant G#5 \ MIDI  68  -  415.31  Hz
#1750 constant A5  \ MIDI  69  -  440.00  Hz
#1767 constant A#5 \ MIDI  70  -  466.16  Hz
#1783 constant B5  \ MIDI  71  -  493.88  Hz
#1798 constant C6  \ MIDI  72  -  523.25  Hz
#1812 constant C#6 \ MIDI  73  -  554.37  Hz
#1825 constant D6  \ MIDI  74  -  587.33  Hz
#1837 constant D#6 \ MIDI  75  -  622.25  Hz
#1849 constant E6  \ MIDI  76  -  659.26  Hz
#1860 constant F6  \ MIDI  77  -  698.46  Hz
#1871 constant F#6 \ MIDI  78  -  739.99  Hz
#1881 constant G6  \ MIDI  79  -  783.99  Hz
#1890 constant G#6 \ MIDI  80  -  830.61  Hz
#1899 constant A6  \ MIDI  81  -  880.00  Hz
#1907 constant A#6 \ MIDI  82  -  932.32  Hz
#1915 constant B6  \ MIDI  83  -  987.77  Hz
#1923 constant C7  \ MIDI  84  - 1046.5   Hz
#1930 constant C#7 \ MIDI  85  - 1108.7   Hz
#1936 constant D7  \ MIDI  86  - 1174.7   Hz
#1943 constant D#7 \ MIDI  87  - 1244.5   Hz
#1949 constant E7  \ MIDI  88  - 1318.5   Hz
#1954 constant F7  \ MIDI  89  - 1396.9   Hz
#1959 constant F#7 \ MIDI  90  - 1480.0   Hz
#1964 constant G7  \ MIDI  91  - 1568.0   Hz
#1969 constant G#7 \ MIDI  92  - 1661.2   Hz
#1974 constant A7  \ MIDI  93  - 1760.0   Hz
#1978 constant A#7 \ MIDI  94  - 1864.7   Hz
#1982 constant B7  \ MIDI  95  - 1975.5   Hz
#1985 constant C8  \ MIDI  96  - 2093.0   Hz
#1988 constant C#8 \ MIDI  97  - 2217.5   Hz
#1992 constant D8  \ MIDI  98  - 2349.3   Hz
#1995 constant D#8 \ MIDI  99  - 2489.0   Hz
#1998 constant E8  \ MIDI 100  - 2637.0   Hz
#2001 constant F8  \ MIDI 101  - 2793.8   Hz
#2004 constant F#8 \ MIDI 102  - 2960.0   Hz
#2006 constant G8  \ MIDI 103  - 3136.0   Hz
#2009 constant G#8 \ MIDI 104  - 3322.4   Hz
#2011 constant A8  \ MIDI 105  - 3520.0   Hz
#2013 constant A#8 \ MIDI 106  - 3729.3   Hz
#2015 constant B8  \ MIDI 107  - 3951.1   Hz

: split-note-freq ( u -- c1 c2 )
  dup
  %11100000000 and 8 rshift swap
  %00011111111 and ;

: note ( u -- )
  split-note-freq
  %00000000 rNR10 c!
  %10000000 rNR11 c!
  %10000000 rNR12 c!
  ( lsb )   rNR13 c!
  ( msb ) %11000000 or
            rNR14 c! ;
