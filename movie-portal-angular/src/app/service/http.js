import angular from 'angular';

class Http {
    constructor($http, $q, $log, apiHost) {
        this.$http = $http;
        this.$q = $q;
        this.$log = $log;

        this.numLoading = 0;
        this.apiHost = apiHost;
    }

    getNumLoading() {
        return this.numLoading;
    }

    get(url, config) {
        url = this.apiHost + url;
        return this._wrap(this.$http.get(url, config));
    }

    _wrap(promise) {
        this.numLoading++;
        return promise.then(
            response => {
                this.numLoading--;
                const responseData = response.data;
                this.$log.debug(`Status code ${response.status}`);
                this.$log.debug(responseData);
                return responseData;
            },
            response => {
                this.numLoading--;
                this.$log.debug(`Status code ${response.status}`);
                return this.$q.reject(response.data);
            }
        );
    }
}

Http.$inject = ['$http', '$q', '$log', 'apiHost'];

export default angular.module('service.http', []).service('http', Http).name;