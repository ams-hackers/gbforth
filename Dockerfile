FROM oldmankris/alpine-gforth

COPY gbforth /app/gbforth
COPY gbforth.fs /app/gbforth.fs
COPY src /app/src
COPY lib /app/lib
COPY shared /app/shared

WORKDIR /data
ENV GBFORTH_PATH /app/lib

ENTRYPOINT ["/app/gbforth"]
