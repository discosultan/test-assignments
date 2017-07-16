// Load global styles through javascript.
import 'foundation-sites/dist/css/foundation.css';

import {createBrowserHistory} from 'history';
import React from 'react';
import {render} from 'react-dom';
import {Provider} from 'react-redux';
import {Route} from 'react-router-dom';
import {ConnectedRouter} from 'react-router-redux';

import configStore from './config/store';

import App from './containers/App';
import HomePage from './containers/HomePage';
import MoviesPage from './containers/MoviesPage';

// Configure redux store for application state and history for router.
const history = createBrowserHistory();
const store = configStore(history);

// Attach app to DOM.
render(
    <Provider store={store}>
        <ConnectedRouter history={history}>
            <App>
                <Route exact path="/" component={HomePage} />
                <Route path="/movies" component={MoviesPage} />
            </App>
        </ConnectedRouter>
    </Provider>,
    document.getElementById('app')
);
