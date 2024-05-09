true [if]
    : foo 1 ; \ foo
[then]

false [if]
    : foo 2 ;
[endif]

true [if]
    : bar 3 ; \ bar
[else]
    : bar 4 ;
[then]

false [if]
    : baz 5 ;
[else]
    : baz 6 ; \ baz
[endif]

true [if]
    false [if]
        : main baz bar foo ;
    [else]
        true  [if] : main foo bar baz ; [then] \ main
        false [if] : main bar foo baz ; [then]
    [endif]
[endif]