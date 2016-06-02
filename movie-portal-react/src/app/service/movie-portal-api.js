export const ApiLoadingChangedEvent = 'api-loading-changed';

export default class MoviePortalApi {
    constructor(eventAggregator, apiHost) {
        this.eventAggregator = eventAggregator;
        this.apiHost = apiHost;
        this.numLoading = 0;
    }
    
    get(url) { return this.fetch(url, { method: 'GET' }) }
    post(url, body) { return this.fetch(url, { method: 'POST', body: body })}
    put(url, body) { return this.fetch(url, { method: 'PUT', body: body })}
    delete(url) { return this.fetch(url, { method: 'DELETE' })}
    
    fetch(url, options) {
        addToLoading.call(this, +1);
        this.eventAggregator.publish(ApiLoadingChangedEvent, { numLoading: this.numLoading });
        url = this.apiHost + url;
        return delayPromise(2500).then(() => fetch(url, options).then(
            response => {
                addToLoading.call(this, -1);
                return response.json();
            },
            response => {
                addToLoading.call(this, -1);
                return Promise.reject(response.message);
            }
        ));
    }
}

function addToLoading(amount) {
    this.numLoading += amount;
    this.eventAggregator.publish(ApiLoadingChangedEvent, { numLoading: this.numLoading });
}

// Simulate a long running task.
function delayPromise(timeout) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, timeout);
    });
}