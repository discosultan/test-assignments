import { Server } from 'http';
import * as Koa from 'koa';
import * as koaBody from 'koa-body';
import * as Router from 'koa-router';
import { Connection, ConnectionOptions, createConnection } from 'typeorm';

import registerImageRoutes from './route/image';
import registerUserRoutes from './route/user';

export default class App {
    private constructor(public connection: Connection, public server: Server) { }

    public async close() {
        this.server.close();
        await this.connection.close();
    }

    public static async run(connectionOptions: ConnectionOptions): Promise<App> {
        const connection = await createConnection(connectionOptions);

        const router = [
            registerUserRoutes,
            registerImageRoutes
        ].reduce((r, f) => f(r), new Router());

        const port = 8000;
        const server = new Koa()
            .use(koaBody({ multipart: true }))
            .use(router.routes())
            .listen(port);
        console.log(`Server running on port ${port}.`);

        return new App(connection, server);
    }
}
