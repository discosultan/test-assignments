import angular from 'angular';

import http from '../service/http';
import movie from '../component/movie';
import moviesList from '../component/movies-list';

const moviesPage = {
    template: require('./movies-page.html'),
    controller: function (http) {
        this.onSelectMovie = movie => http.get(`movies/${movie.id}`).then(movie => this.movie = movie);

        http.get('categories').then(categories => this.categories = categories);
        http.get('movies').then(movies => {
            this.movies = movies;
            // Select the first movie by default.
            if (movies.length) {
                this.onSelectMovie(movies[0]);
            }
        });
    }
};

export default angular.module('component.movies-page', [movie, moviesList, http])
    .component('moviesPage', moviesPage)
    .name;