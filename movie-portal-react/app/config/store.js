import {createStore, combineReducers, applyMiddleware} from 'redux';
import thunk from 'redux-thunk';
import createLogger from 'redux-logger';
import {routerReducer} from 'react-router-redux';

import * as appReducers from '../containers/App/reducers';
import moviesPageReducers from '../containers/MoviesPage/reducers';

export default function configStore() {
    const rootReducer = combineReducers({
        ...appReducers,
        ...moviesPageReducers,
        routing: routerReducer // Add the reducer to your store on the `routing` key.
    });
    const initialState = undefined;
    const middleware = applyMiddleware(thunk, createLogger());

    return createStore(rootReducer, initialState, middleware);
}
