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

## Assignment

Secfi is migrating from a monolithic project to microservices. Next up is writing a microservice that can store our users securely. The challenge is to design a microservice that allows CRUD operations on a user table in a self defined database. The service should allow read and write operations following this interface:

```ts
interface IUser {
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
  avatar: string;
}
```

The requirements are as follows:

- Service should be written in TypeScript
- Service should use TypeORM (http://typeorm.io)
- Service should be run with Docker
- Database can be any type (Postgresql, MySQL, SQLite)
- Password should be stored in encrypted format
- For the avatar, pick a way of storing you think is most efficient
