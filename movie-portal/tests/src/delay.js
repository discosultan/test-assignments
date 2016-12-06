function delay(ms) {
    return new Promise(function (resolve, reject) {
        setTimeout(function () { return resolve(); }, ms);
    });
}
exports.delay = delay;
