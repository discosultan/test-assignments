import RestApi from './RestApi';

const cache = {};

export default class CachingRestApi extends RestApi {
    get(url) {
        // Only do a network request if the resource has not been requested before.
        const cacheKey = url.replace('/', '_');
        const existingResult = cache[cacheKey];

        if (existingResult)
            return Promise.resolve(existingResult);
            
        return super.get(url).then(result => {
            cache[cacheKey] = result;
            return result;
        });
    }
}
