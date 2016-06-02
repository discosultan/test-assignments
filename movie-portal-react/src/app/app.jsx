import 'foundation-sites/dist/foundation.css';

import React from 'react';
import { render } from 'react-dom';
import { Router, Route, browserHistory } from 'react-router';

import MainLayout from './layout/main-layout.jsx';
import HomeLayout from './layout/home-layout.jsx';
import MoviesLayout from './layout/movies-layout.jsx';

import Api from './service/api.js';
import EventAggregator from './service/event-aggregator.js';

// Config params.
const apiHost = 'http://40.113.15.185:3000/';

// App root component.
class App extends React.Component {
  // Dependency resolution.
  getChildContext() {
    const eventAggregator = new EventAggregator();
    const api = new Api(eventAggregator, apiHost);
    return {
      eventAggregator: eventAggregator,
      api: api
    };
  }

  // Routes config.
  render() {
    return (
      <Router history={browserHistory}>
        <Route component={MainLayout}>
          <Route path="/" component={HomeLayout} />
          <Route path="/movies" component={MoviesLayout} />
        </Route>
      </Router>
    );
  }
}

// Define the type of context objects to pass down the component subtree.
App.childContextTypes = {
  api: React.PropTypes.object,
  eventAggregator: React.PropTypes.object
};

// Attach app to DOM.
render(<App />, document.getElementById('app'));