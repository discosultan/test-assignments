import { Server } from 'http';
import * as path from 'path';
import * as request from 'supertest';
import { getConnectionOptions } from 'typeorm';
import * as uuidv4 from 'uuid/v4';

import App from '../src/app';

let app: App;
let server: Server;

beforeEach(async () => {
    const connectionOptions = await getConnectionOptions();
    Object.assign(connectionOptions, {
        database: ':memory:'
    });
    // TODO: Figure out how to run the server without consuming an OS port.
    app = await App.run(connectionOptions);
    server = app.server;
});

afterEach(() => app.close());

describe('routes: /user', () => {
    test('should respond as expected', async () => {
        const user = {
            userName: 'aeropleustic',
            firstName: 'ursicide',
            lastName: 'mogadore',
            password: 'glockenspiel',
            avatar: 'gules'
        };

        // POST.
        let response = await request(server).post('/user').send(user);
        expect(response.status).toEqual(200);

        // GET all.
        response = await request(server).get('/user');
        expect(response.status).toEqual(200);
        const receivedUser = response.body[0];
        expect(exceptPassword(receivedUser)).toEqual(exceptPassword(user));

        // PUT.
        const modify = {
            firstName: 'sanguiferous'
        };
        response = await request(server).put(`/user/${user.userName}`).send(modify);
        expect(response.status).toEqual(200);

        // GET one.
        response = await request(server).get(`/user/${user.userName}`);
        expect(response.status).toEqual(200);
        expect(exceptPassword(response.body)).toEqual(exceptPassword({ ...user, ...modify }));

        // DELETE.
        response = await request(server).delete(`/user/${user.userName}`);
        expect(response.status).toEqual(200);

        // GET all.
        response = await request(server).get('/user');
        expect(response.status).toEqual(200);
        expect(response.body).toEqual([]);
    });
});

describe('routes: /image', () => {
    test('should respond as expected', async () => {
        const invalidId = uuidv4();

        // GET.
        let response = await request(server).get(`/image/${invalidId}`);
        expect(response.status).toEqual(404);

        // POST.
        response = await request(server).post('/image')
            .attach('data', path.join(__dirname, 'dummy_img.png'));
        expect(response.status).toEqual(200);
        const id = response.body.id;

        // GET.
        response = await request(server).get(`/image/${id}`);
        expect(response.status).toEqual(200);
    });
});

function exceptPassword(obj: any): any {
    const clone = { ...obj };
    delete clone['password'];
    return clone;
}
