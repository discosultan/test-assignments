import {combineReducers} from 'redux';
import undoable, {distinctState} from 'redux-undo';

function movies(state = [], action) {
    switch (action.type) {
        case 'RECEIVE_MOVIES':
            return action.movies;
        default:
            return state;
    }
}

function categories(state = [], action) {
    switch (action.type) {
        case 'RECEIVE_CATEGORIES':
            return action.categories;
        default:
            return state;
    }
}

function selectedMovieId(state = null, action) {
    switch (action.type) {
        case 'SELECT_MOVIE':
            return action.id;
        case 'RECEIVE_MOVIES':
            return action.movies[0].id || null;
        default:
            return state;
    }
}

function movieDetailsMap(state = {}, action) {
    switch (action.type) {
        case 'RECEIVE_MOVIE_DETAILS':
            return Object.assign({}, state, {
                [action.movieDetails.id]: action.movieDetails
            });
        default:
            return state;
    }
}

function categoryFilter(state = [], action) {
    switch (action.type) {
        case 'SET_CATEGORY_FILTER':
            return action.filter;
        default:
            return state;
    }
}

function searchFilter(state = '', action) {
    switch (action.type) {
        case 'SET_SEARCH_FILTER':
            return action.filter;
        default:
            return state;
    }
}

// To make a state change undoable, simply wrap it using `undoable` helper.
// The `distinctState` filter defines only to undo distinct changes to the state.
const undoableSelectedMovieId = undoable(selectedMovieId, { filter: distinctState() });

export default combineReducers({
    movies,
    categories,
    // selectedMovieId,
    selectedMovieId: undoableSelectedMovieId,
    movieDetailsMap,
    categoryFilter,
    searchFilter
});
