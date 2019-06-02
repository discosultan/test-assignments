import * as Router from 'koa-router';

import User from '../entity/User';
import { hashPassword } from '../util/crypto';
import { badRequest, notFound, ok } from '../util/http';

export default function register(router: Router): Router {
    // TODO: Input validation.
    return router
        .get('/user', async ctx => {
            ctx.body = await User.find();
        })
        .get('/user/:userName', async ctx => {
            ctx.body = await User.findOne(ctx.params.userName);
            if (!ctx.body) return notFound(ctx, `User ${ctx.params.userName} not found.`);
        })
        .post('/user', async ctx => {
            const user = new User();
            user.userName = ctx.request.body.userName;
            user.firstName = ctx.request.body.firstName;
            user.lastName = ctx.request.body.lastName;
            user.password = await hashPassword(ctx.request.body.password);
            user.avatar = ctx.request.body.avatar;

            try {
                await User.insert(user);
            } catch {
                return badRequest(ctx, `User name ${user.userName} already taken.`);
            }
            return ok(ctx);
        })
        .put('/user/:userName', async ctx => {
            await User.update(
                { userName: ctx.params.userName },
                ctx.request.body);
            return ok(ctx);
        })
        .del('/user/:userName', async ctx => {
            const result = await User.delete(ctx.params.userName);
            return ok(ctx, `Affected ${result.affected}.`);
        });
}
