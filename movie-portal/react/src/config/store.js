import {createStore, combineReducers, applyMiddleware} from 'redux';
import {createLogger} from 'redux-logger';
import {routerReducer, routerMiddleware} from 'react-router-redux';
import undoable, {distinctState} from 'redux-undo';

import appReducer from '../containers/App/reducer';
import moviesPageReducer from '../containers/MoviesPage/reducer';

import RestApi from '../utils/CachingRestApi';
import createApi from '../utils/apiMiddleware';

export default function configStore(history) {
    const rootReducer = combineReducers({
        router: routerReducer, // Add the reducer to your store on the `router` key.
        app: appReducer,
        moviesPage: moviesPageReducer
        // To make a state change undoable, simply wrap it using `undoable` helper.
        // The `distinctState` filter defines only to undo distinct changes to the state.
        // moviesPage: undoable(moviesPageReducer, { filter: distinctState() })
    });
    const initialState = undefined;
    const middleware = applyMiddleware(
        routerMiddleware(history),
        createApi(new RestApi('http://40.113.15.185:3000/')),
        createLogger()
    );

    return createStore(
        rootReducer,
        initialState,
        middleware
    );
}
