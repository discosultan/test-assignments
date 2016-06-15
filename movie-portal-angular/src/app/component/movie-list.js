import angular from 'angular';

import inArray from '../filter/in-array';
import http from '../service/http';

const movieList = {
    template: require('./movie-list.html'),
    controller: function (http) {
        http.get('categories').then(categories => this.categories = categories);
        http.get('movies').then(movies => this.movies = movies);
    }
};

export default angular.module('component.movie-list', [inArray, http])
    .component('movieList', movieList)
    .name;
