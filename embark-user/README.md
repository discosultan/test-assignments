# embark-user

User management service for Embark Inc.

## Getting started

```sh
$ npm install
$ npm start
```

Note that the application makes use of [bcrypt](https://github.com/kelektiv/node.bcrypt.js/) which
may require additional setup depending on the OS platform. Please refer to bcrypt repo for
guidance.

For development, to automatically reload app:

```sh
$ npm run watch
```

For building and running in Docker container:

```sh
$ docker build -t embark-user .
$ docker run -p 8000:8000 embark-user
```

For running tests:

```sh
$ npm test
```

## TODO

* Use `ts-node-dev` instead of `ts-node` + `nodemon` for faster development cycle.
* If we care about startup performance, we would pre-compile TypeScript to JavaScript and run
  `node` directly.
