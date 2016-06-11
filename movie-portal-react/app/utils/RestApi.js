function checkStatus(response) {
    if (response.status >= 200 && response.status < 300) {
        return response;
    }

    const error = new Error(response.statusText);
    error.response = response;
    throw error;
}

function delay(timeout) {
    return response => {
        return new Promise((resolve, reject) => {
            setTimeout(() => {
                resolve(response);
            }, timeout);
        });
    }
}

function parseJSON(response) {
    return response.json();
}

export default class RestApi {
    constructor(apiHost) {
        this.apiHost = apiHost;
    }

    get(url) { return this.fetch(url, { method: 'GET' }) }
    post(url, body) { return this.fetch(url, { method: 'POST', body: body })}
    put(url, body) { return this.fetch(url, { method: 'PUT', body: body })}
    delete(url) { return this.fetch(url, { method: 'DELETE' })}

    fetch(url, options) {
        url = this.apiHost + url;
        return fetch(url, options)
            .then(checkStatus)
            .then(delay(2000)) // Simulate a long running task.
            .then(parseJSON);
            // .then(data => (data }))
            // .catch(err => ({ err }));
    }
}
