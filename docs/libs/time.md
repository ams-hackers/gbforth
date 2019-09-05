# time.fs (lib) [☞](https://github.com/ams-hackers/gbforth/blob/master/lib/time.fs)

The `time.fs` library contains (very inaccurate) words that help keep track of time.

## Word Index

##### `ms` _( u -- )_

Wait for approximately _u_ milliseconds. This delay is based on a simple busy
loop (not actual timers) so the actual waiting time will not be very accurate.

##### `utime` _( -- u )_

Returns the time in microseconds since the last wrapping of the divider
register. This value increments with approximately `61` microseconds per tick
and until it reaches the maximum value of `15555`.
