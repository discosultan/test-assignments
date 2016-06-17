import RestApi from './RestApi';

const requestPrefix = 'REQUEST_';
const responsePrefix = 'RECEIVE_';

/**
 * Lets you dispatch async actions with types prefixed with requestPrefix.
 * Automatically fires a similar action with the result of the request
 * once the request is completed.
 *
 * action: {
 *    type: string;    // 'REQUEST_SOMETHING'
 *    method?: string; // 'GET', 'POST', 'PUT', 'DELETE' // 'GET' (default)
 *    uri?: string;    // 'movies/5'                     // 'SOMETHING'*
 *    body?: any;      // { id: 5 }                      // undefined
 * }
 */
export default apiHost => {
    const moviePortalApi = new RestApi(apiHost);
    const cache = {};

    return store => next => action => {
        if (!action.type.startsWith(requestPrefix)) {
            return next(action);
        }

        next(action);

        // Here I wish we had null propagation operator '?.' similar to C#.
        const requestMethod = (action.method || 'get').toLowerCase();
        const requestUri = action.uri || action.type.substring(requestPrefix.length);
        const requestBody = action.body;

        // Only do a network request if the resource has not been requested before.
        const cacheKey = requestUri.replace('/', '_');
        const existingResult = cache[cacheKey];
        if (existingResult) return next(getResponseAction(action, existingResult));

        // Fetch the result from network.
        return moviePortalApi[requestMethod](requestUri, requestBody)
            .then(result => {
                cache[cacheKey] = result;
                next(getResponseAction(action, result));
            });
    };
};

function getResponseAction(action, result) {
    const responseActionType = action.type.startsWith(requestPrefix)
        ? `${responsePrefix}${action.type.substring(requestPrefix.length)}`
        : `${responsePrefix}${action.type}`;
    return {
        type: responseActionType,
        result: result
    }
}
