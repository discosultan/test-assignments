import angular from 'angular';

import inArray from '../filter/in-array';

const moviesList = {
    // = - two-way binding
    // < - one-way binding
    // @ - string binding
    // & - callback binding
    bindings: {
        categories: '<',
        movies: '<',
        onselect: '&'
    },
    template: require('./movies-list.html')    
};

export default angular.module('component.movies-list', [inArray])
    .component('moviesList', moviesList)
    .name;
