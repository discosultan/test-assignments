import {createStore, combineReducers, applyMiddleware} from 'redux';
import thunk from 'redux-thunk';
import createLogger from 'redux-logger';
import {routerReducer} from 'react-router-redux';
import undoable, {distinctState} from 'redux-undo';

import appReducer from '../containers/App/reducer';
import moviesPageReducer from '../containers/MoviesPage/reducer';

export default function configStore() {
    const rootReducer = combineReducers({
        routing: routerReducer, // Add the reducer to your store on the `routing` key.
        app: appReducer,
        moviesPage: moviesPageReducer
        // To make a state change undoable, simply wrap it using `undoable` helper.
        // The `distinctState` filter defines only to undo distinct changes to the state.
        // moviesPage: undoable(moviesPageReducer, { filter: distinctState() })
    });
    const initialState = undefined;
    const middleware = applyMiddleware(thunk, createLogger());

    return createStore(
        rootReducer,
        initialState,
        middleware
    );
}
