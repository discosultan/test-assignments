var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments)).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t;
    return { next: verb(0), "throw": verb(1), "return": verb(2) };
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var _this = this;
var tape_async_1 = require("./util/tape-async");
var movie_portal_api_1 = require("./movie-portal-api");
var api = new movie_portal_api_1.MoviePortalApi('http://40.113.15.185:3000');
tape_async_1.default('listMoviePreviews', function (t) { return __awaiter(_this, void 0, void 0, function () {
    var moviePreviews;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0: return [4 /*yield*/, api.listMoviePreviews()];
            case 1:
                moviePreviews = _a.sent();
                t.true(moviePreviews.length > 0);
                t.deepEqual({
                    id: 1,
                    title: 'Harry Potter',
                    category: 'fantasy',
                    rating: 5,
                    year: '2014'
                }, moviePreviews[0]);
                return [2 /*return*/];
        }
    });
}); });
tape_async_1.default('listMovie', function (t) { return __awaiter(_this, void 0, void 0, function () {
    var movie;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0: return [4 /*yield*/, api.getMovie(1)];
            case 1:
                movie = _a.sent();
                t.deepEqual({
                    id: 1,
                    title: 'Harry Potter',
                    category: 'fantasy',
                    rating: 5,
                    year: '2014',
                    description: 'This is very cool movie about young wizard.'
                }, movie);
                return [2 /*return*/];
        }
    });
}); });
tape_async_1.default('listCategories', function (t) { return __awaiter(_this, void 0, void 0, function () {
    var categories;
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0: return [4 /*yield*/, api.listCategories()];
            case 1:
                categories = _a.sent();
                t.true(categories.length > 0);
                t.deepEqual({
                    id: 'fantasy',
                    name: 'Fantasy'
                }, categories[0]);
                return [2 /*return*/];
        }
    });
}); });
