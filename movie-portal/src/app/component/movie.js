import angular from 'angular';
import http from '../service/http';

let movie = {
    bindings: {
        id: '<'
    },
    template: require('./movie.html'),
    controller: function (http) {        
        http.get(`movies/${this.id}`).then(movie => this.movie = movie);
    }
};

export default angular.module('component.movie', [http])
    .component('movie', movie)
    .name;