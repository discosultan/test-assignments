import 'foundation-sites/dist/foundation.css';

import React, { Component } from 'react';
import { render } from 'react-dom';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import { Router, Route, browserHistory } from 'react-router';
import { syncHistoryWithStore, routerReducer } from 'react-router-redux';

import App from './containers/App.jsx';
import HomePage from './containers/HomePage.jsx';
import MoviesPage from './containers/MoviesPage.jsx';

import MoviePortalApi from './utils/movie-portal-api.js';

import reducers from './reducers/index.js';

// Config params.
// const initialState = {
//   numLoading: 0,
//   api: new MoviePortalApi('http://40.113.15.185:3000/')
// }

// Add the reducer to your store on the `routing` key
const store = createStore(
  combineReducers({
    ...reducers,
    routing: routerReducer
  })
);

const history = syncHistoryWithStore(browserHistory, store);

// Attach app to DOM.
render(
  <Provider store={store}>
    <Router history={history}>
      <Route component={App}>
        <Route path="/" component={HomePage} />
        <Route path="/movies" component={MoviesPage} />
      </Route>
    </Router>
  </Provider>,
  document.getElementById('app')
);
