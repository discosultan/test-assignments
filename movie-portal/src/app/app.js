import 'foundation-sites/dist/foundation.css';

import angular from 'angular';
import uiRouter from 'angular-ui-router';

import config from './app.config';

import movie from './component/movie';
import movieList from './component/movie-list';

export default angular.module('app', [uiRouter, movie, movieList])
    .config(config.routing)
    .value('apiHost', config.apiHost)
    .name;