import * as Koa from 'koa';

export const ok = status(200);
export const badRequest = status(400);
export const notFound = status(404);

function status(status: number) {
    return function(
        ctx: Koa.ParameterizedContext,
        data: any = undefined,
        type: string | undefined = undefined) {

        ctx.status = status;
        ctx.body = typeof (data) == 'string' ? { 'message': data } : data || {};
        if (type) {
            ctx.type = type;
        }
    }
}
