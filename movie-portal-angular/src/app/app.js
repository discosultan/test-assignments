import 'foundation-sites/dist/foundation.css';

import angular from 'angular';
import uiRouter from 'angular-ui-router';

import { apiHost, routing } from './app.config';
import moviesPage from './container/movies-page';

export default angular.module('app', [uiRouter, moviesPage])
    .config(routing)
    .value('apiHost', apiHost)
    .name;
