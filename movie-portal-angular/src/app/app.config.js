export const apiHost = 'http://40.113.15.185:3000/';

export const routing = ($stateProvider, $urlRouterProvider, $locationProvider) => {
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/movie-list');

    $stateProvider
        .state('movie-list', {
            component: 'movieList',
            url: '/movie-list'
        })
        .state('movie', {
            component: 'movie',
            url: '/movie/:id',
            resolve: {
                id: $stateParams => $stateParams.id
            }
        });
}
routing.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];