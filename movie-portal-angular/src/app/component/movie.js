import angular from 'angular';

const movie = {    
    bindings: {
        value: '<'
    },
    template: require('./movie.html')    
};

export default angular.module('component.movie', [])
    .component('movie', movie)
    .name;
