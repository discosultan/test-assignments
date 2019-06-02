import * as Router from 'koa-router';
import * as uuidv4 from 'uuid/v4';

import Image from '../entity/Image';
import { notFound, ok } from '../util/http';
import { moveToHome, readFile } from '../util/fs';

export default function register(router: Router): Router {
    // TODO: Input validation.
    return router
        .get('/image/:id', async ctx => {
            const image = await Image.findOne(ctx.params.id);
            if (!image) return notFound(ctx, `Image ${ctx.params.id} not found.`);

            return ok(ctx, await readFile(image.id), image.type);
        })
        .post('/image', async ctx => {
            const file = ctx.request.files.data;

            const image = new Image();
            image.id = uuidv4();
            image.name = file.name;
            image.size = file.size;
            image.type = file.type;

            await Promise.all([
                // Move the image file from temp to app home.
                // TODO: It would be more efficient to handle network stream directly and write the
                // file straight to app home dir.
                // TODO: Abstract over fs operations so we wouldn't access disk during tests.
                moveToHome(file.path, image.id),
                Image.insert(image)
            ]);

            return ok(ctx, { 'id': image.id });
        });
}
