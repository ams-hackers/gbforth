# Setup

You'll need to install [gforth](https://www.gnu.org/software/gforth/) to be
able to run gbforth.

On mac, you can use `brew`:

```
brew install gforth
```

## Docker

Alternatively, you can run gbforth in a Docker container. This does not require
you to have any other dependencies (apart from Docker) installed. Use the
[amshackers/gbforth](https://hub.docker.com/r/amshackers/gbforth) docker image:

```
docker run amshackers/gbforth
```

To compile your project, mount your project directory as a volume and
pass the input and output file as [CLI](./cli.md) arguments:

```
docker run --rm -v "$PWD":/data amshackers/gbforth game.fs game.gb
```
