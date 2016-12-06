function delay(milliseconds) {
    return new Promise(function (resolve) {
        setTimeout(resolve, milliseconds);
    });
}
Object.defineProperty(exports, "__esModule", { value: true });
exports.default = delay;
