var test = require("tape");
function asyncTest(name, cb) {
    test(name, function (t) {
        cb(t).then(function () { return t.end(); });
    });
}
exports.asyncTest = asyncTest;
