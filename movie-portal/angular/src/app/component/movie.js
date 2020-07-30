import angular from 'angular';

const movie = {
    // = - two-way binding
    // < - one-way binding
    // @ - string binding
    // & - callback binding
    bindings: {
        value: '<'
    },
    template: require('./movie.html')
};

export default angular.module('component.movie', [])
    .component('movie', movie)
    .name;
