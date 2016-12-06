import * as test from 'tape'

export default function asyncTest(name: string, cb: (t: test.Test) => Promise<any>): void {
    test(name, t => {
        cb(t).then(() => t.end());        
    });
}