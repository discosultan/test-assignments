// Load global styles through javascript.
import 'foundation-sites/dist/foundation.css';
import './styles.css';

import React from 'react';
import {render} from 'react-dom';
import {Provider} from 'react-redux';
import {Router, Route} from 'react-router';

import configStore from './config/store';
import configHistory from './config/history';

import App from './containers/App';
import HomePage from './containers/HomePage';
import MoviesPage from './containers/MoviesPage';

const store = configStore();
const history = configHistory(store);

// Attach app to DOM.
render(
    <Provider store={store}>
        <Router history={history}>
            <Route component={App}>
                <Route path="/" component={HomePage}/>
                <Route path="/movies" component={MoviesPage}/>
            </Route>
        </Router>
    </Provider>,
    document.getElementById('app')
);
