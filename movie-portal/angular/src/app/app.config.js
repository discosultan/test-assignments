export const apiHost = 'http://40.113.15.185:3000/';

export const routing = ($stateProvider, $urlRouterProvider, $locationProvider) => {
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/movies');

    $stateProvider
        // We only have a single page that lists all the movies and displays
        // selected movie details.
        .state('movies', {
            component: 'moviesPage',
            url: '/movies'
        });
}
routing.$inject = ['$stateProvider', '$urlRouterProvider', '$locationProvider'];