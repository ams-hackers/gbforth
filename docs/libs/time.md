# time.fs (lib)

The `time.fs` library contains (very inaccurate) words that help keep track of time.

## Word Index

##### `ms` _( n -- )_

Wait for approximately _n_ milliseconds. This delay is based on a simple busy
loop (not actual timers) so the actual waiting time will not be very accurate.

##### `utime` _( -- n )_

Returns the time in microseconds since the last wrapping of the divider
register. This value increments with approximately `61` microseconds per tick
and until it reaches the maximum value of `15555`.
